using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Vector3 center;
    [SerializeField] float bulletRange;

    [SerializeField] GameObject bulletMark;
    [SerializeField] GameObject muzzleParticle;
    
    [SerializeField] Transform muzzle;

    [SerializeField] LayerMask bulletLayer;

    void Start()
    {
        center = new Vector3(Screen.width / 2, Screen.height / 2);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
       
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

        Instantiate(muzzleParticle, muzzle.position, Quaternion.identity);
    }
}
