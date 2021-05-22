using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public EnemyLife enemy;

    public float range = 10;
    public float fireRate = 0.1f;
    public float timeToReload = 5;

    Transform turretBody;
    Transform target;

    public Transform muzzle;

    float cooldown;

    bool shooting;

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

    //IEnumerator ShootingTime()
    //{
    //    if (shooting) yield break;

    //    shooting = true;

    //    Shoot(fireRate);

    //    yield return new WaitForSeconds(timeToReload);

    //    shooting = false;
    //}


    void Shoot(float _fireRate)
    {
        if (cooldown < Time.time)
        {
            cooldown = Time.time + _fireRate;

            Vector3 position = muzzle.position;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, muzzle.forward);

            GameObject prefab = Instantiate(projectilePrefab, position, rotation);

            prefab.GetComponent<Rigidbody>().AddForce(muzzle.forward * 500);

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
