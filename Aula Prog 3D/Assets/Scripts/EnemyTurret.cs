using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float range;

    Transform muzzle;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.Find("Muzzle");

        target = FindObjectOfType<PlayerVision>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < range)
        {
            transform.LookAt(target);

            Vector3 position = muzzle.position;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, muzzle.forward);

            GameObject prefab = Instantiate(projectilePrefab, position, rotation);

            prefab.GetComponent<Rigidbody>().AddForce(muzzle.forward * 500);

            Destroy(prefab, 3);
        }
    }


    void Shoot()
    {

    }
}
