using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    Vector3 center;
    int currentAmmo;
    bool isReloading;

    Animator gunAnim;

    [SerializeField] bool isAutomatic;
 
    [SerializeField] int magAmmo = 10;
    [Range(0, 200)] int totalAmmo = 30;

    [SerializeField] Text ammoText;
    
    [SerializeField] float bulletRange;
    [SerializeField] float fireRate = 1;

    [SerializeField] GameObject bulletMark;
    [SerializeField] ParticleSystem muzzleParticle;
    
    [SerializeField] Transform muzzle;

    [SerializeField] LayerMask bulletLayer;

    float cooldown = 0;

    void Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2);

        currentAmmo = magAmmo;

        gunAnim = GetComponent<Animator>();

        totalAmmo = magAmmo * 4;
    }
    
    void Update()
    {

        ammoText.text = currentAmmo.ToString() + "/" + totalAmmo.ToString();
        
        if (isReloading)
            return;

        if (!isAutomatic)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentAmmo > 0)
                    Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= cooldown)
            {
                if (currentAmmo > 0)
                {
                    Shoot();
                    cooldown = Time.time + 1f / fireRate;
                }
                    
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
            if (currentAmmo < magAmmo && totalAmmo > 0)
            {
                StartCoroutine(ReloadAmmo());
                return;
            }        
        }       
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
            if (isAutomatic)
            {
                if (cooldown < Time.time)
                {
                    cooldown = Time.time + 1f;
                }
            }

            --currentAmmo;

            Quaternion markSurface = Quaternion.LookRotation(-hit.normal);

            Vector3 markOffset = hit.point + hit.normal / 100;

            GameObject bulletPrefab = Instantiate(bulletMark, markOffset, markSurface);
            Destroy(bulletPrefab, 3);

            if (muzzleParticle)
                muzzleParticle.Play();             
        }

    }

    public void ReceiveAmmo(int ammo)
    {
        if(totalAmmo < 200)
            totalAmmo += ammo;
    }

    IEnumerator ReloadAmmo()
    {
        float reloadTime = gunAnim.runtimeAnimatorController.animationClips.Length;
        
        isReloading = true;

        gunAnim.SetBool("Reloading", true);
        
        yield return new WaitForSeconds(reloadTime);
        
        totalAmmo -= magAmmo - currentAmmo;
            
        currentAmmo = magAmmo; 
        
        isReloading = false;

        gunAnim.SetBool("Reloading", false);
    }
}
