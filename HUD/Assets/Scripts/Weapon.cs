using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Vector3 center;

    int currentAmmo;
    [SerializeField] int magAmmo = 10;
    public int totalAmmo = 30;

    [SerializeField] Text ammoText;
    
    [SerializeField] float bulletRange;

    [SerializeField] GameObject bulletMark;
    [SerializeField] GameObject muzzleParticle;
    
    [SerializeField] Transform muzzle;

    [SerializeField] LayerMask bulletLayer;

    void Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2);

        currentAmmo = magAmmo;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0) 
                Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

        ammoText.text = currentAmmo.ToString() + "/" + totalAmmo.ToString();

    }

    private void OnEnable()
    {
        ammoText.gameObject.SetActive(true);
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, bulletRange, bulletLayer))
        {
            Quaternion markSurface = Quaternion.LookRotation(-hit.normal);

            Vector3 markOffset = hit.point + hit.normal / 100;

            GameObject bulletPrefab = Instantiate(bulletMark, markOffset, markSurface);
            Destroy(bulletPrefab, 3);
        }

        if(muzzleParticle)
            SpawnParticle(muzzle.position, muzzleParticle, 2);

        --currentAmmo;

    }

    void SpawnParticle(Vector3 origin, GameObject prefab, float timeToDestroy)
    {   
        GameObject _prefab = Instantiate(prefab, origin, Quaternion.identity);

        _prefab.SetActive(true);

        Destroy(_prefab, timeToDestroy);
    }

    void ReloadAmmo()
    {
        if(currentAmmo < magAmmo && totalAmmo > 0)
        {
            totalAmmo -= magAmmo - currentAmmo;
            
            currentAmmo = magAmmo; 
        }
        else
        {
            Debug.Log("NO AMMO");
        }
    }
}
