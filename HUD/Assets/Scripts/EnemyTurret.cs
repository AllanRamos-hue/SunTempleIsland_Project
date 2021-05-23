using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public bool fixDirection;
    public bool keepShooting;
    public GameObject projectilePrefab;
    public EnemyLife enemy;

    public float range = 10;
    public float fireRate = 0.1f;
    public float bulletForce = 500;
    public Transform muzzle;

    public AudioClip shootSFX;

    Transform turretBody;
    Transform target;
    
    float cooldown;  

    void Start()
    {
        turretBody = transform.GetChild(0);

        target = FindObjectOfType<PlayerVision>().transform;

    }
    void Update()
    {
        if(EnemyIsAlive())
            DetectPlayer(range);
    }

    void DetectPlayer(float _range)
    {
        if (Vector3.Distance(target.position, transform.position) < _range)
        {
            Ray ray = new Ray(turretBody.position, target.position - turretBody.position);

            RaycastHit hit;

            if(keepShooting)
            {
                transform.LookAt(target);

                Shoot(fireRate);
            }
            else
            {
                if (Physics.Raycast(ray, out hit, _range))
                {

                    if (hit.transform == target)
                    {
                        transform.LookAt(target);

                        Shoot(fireRate);
                    }

                }
            }
        }
    }

    void Shoot(float _fireRate)
    {
        float multiplier = 1;

        if (fixDirection)
            multiplier = -1;
        
        if (cooldown < Time.time)
        {
            cooldown = Time.time + _fireRate;

            Vector3 position = muzzle.position;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, muzzle.forward);

            GameObject prefab = Instantiate(projectilePrefab, position, rotation);

            AudioManager.PlaySFX(shootSFX, 0.05f);

            prefab.GetComponent<Rigidbody>().AddForce(multiplier * muzzle.forward * 500);

            Destroy(prefab, 3);
        }
    }

    bool EnemyIsAlive()
    {
        if(!enemy.gameObject.activeSelf)
        {
            return false;
        }

        return true;
    }
}
