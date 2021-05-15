using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Vector3 center;

    int currentAmmo;
    [SerializeField][Range(0,10)] int magAmmo = 10;
    public int totalAmmo = 30;
    
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
            else
            {
                Debug.Log("NO AMMO");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadAmmo();
        }

    }

    void Shoot()
    {
        --currentAmmo;

        Debug.Log("MA: " + currentAmmo);

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
      
    }

    void SpawnParticle(Vector3 origin, GameObject prefab, float timeToDestroy)
    {   
        GameObject _prefab = Instantiate(prefab, origin, Quaternion.identity);

        _prefab.SetActive(true);

        Destroy(_prefab, timeToDestroy);
    }

    void ReloadAmmo()
    {
        if(currentAmmo < magAmmo)
        {
            currentAmmo = magAmmo;
            Debug.Log("MA: " + currentAmmo);
        }
    }
}
