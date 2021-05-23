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
    [SerializeField] bool isRocket;

    [SerializeField] int magAmmo = 10;
    [Range(0, 200)] int totalAmmo;

    [SerializeField] Text ammoText;
    
    [SerializeField] float bulletRange;
    [SerializeField] float damage = -30;
    [SerializeField] float fireRate = 1;

    [SerializeField] GameObject bulletMark;
    [SerializeField] ParticleSystem muzzleParticle;
    
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject rocket;

    [SerializeField] LayerMask bulletLayer;

    public AudioClip shootSFX;
    public AudioClip noAmmoSFX;
    public AudioClip reloadSFX;

    float cooldown = 0;

    void Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2);

        currentAmmo = magAmmo;

        gunAnim = GetComponent<Animator>();

        totalAmmo = magAmmo * 3;
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
                {
                    if (muzzleParticle)
                        muzzleParticle.Play();

                    Shoot();
                }     
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= cooldown)
            {
                if (currentAmmo > 0)
                {
                    if (muzzleParticle)
                        muzzleParticle.Play();
                    
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

        if(currentAmmo <=0 && totalAmmo <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                AudioManager.PlaySFX(noAmmoSFX);
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

        if (isRocket)
        {
            Vector3 position = muzzle.position;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, muzzle.forward);

            GameObject prefab = Instantiate(rocket, position, rotation);
            Destroy(prefab, 6);
            
            transform.GetChild(0).gameObject.SetActive(false);

            prefab.GetComponent<Rigidbody>().AddForce(-muzzle.forward * 1000);
        }

        if (Physics.Raycast(ray, out hit, bulletRange, bulletLayer))
        {
            if (isAutomatic)
            {
                if (cooldown < Time.time)
                {
                    cooldown = Time.time + 1f;
                }
            }
            
            Quaternion markSurface = Quaternion.LookRotation(-hit.normal);

            Vector3 markOffset = hit.point + hit.normal / 100;

            GameObject bulletPrefab = Instantiate(bulletMark, markOffset, markSurface);
            Destroy(bulletPrefab, 3);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyLife>().TakeDamage(damage);
            }
                 
        }
            
        AudioManager.PlaySFX(shootSFX);
        
        --currentAmmo;
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

        AudioManager.PlaySFX(reloadSFX);

       
        totalAmmo -= magAmmo - currentAmmo;

        currentAmmo = magAmmo;
        
        if (isRocket)
            transform.GetChild(0).gameObject.SetActive(true);

        gunAnim.SetBool("Reloading", false);

        isReloading = false;

    }
}
