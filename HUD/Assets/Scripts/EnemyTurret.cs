using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float range = 10;
    public float fireRate = 0.2f;
    public float timeToReload = 5;

    Transform turretBody;
    Transform target;

    public Transform muzzle;

    float cooldown;

    void Start()
    {
        turretBody = transform.GetChild(0);

        target = FindObjectOfType<PlayerVision>().transform;

    }
    void Update()
    {
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

    //IEnumerator ShootingTime()
    //{
    //    Shoot(fireRate);
        
    //    yield return new WaitForSeconds(8);

    //    StartCoroutine(ReloadTime(timeToReload));
    //}

    //IEnumerator ReloadTime(float _time)
    //{
    //    yield return new WaitForSeconds(_time);

    //    Shoot(fireRate);
    //}
}
