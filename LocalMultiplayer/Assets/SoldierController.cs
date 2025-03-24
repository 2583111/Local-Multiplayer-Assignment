using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public string targetTag = "Player"; // Ensure this tag is correct
    public float fireRate = 2f;
    public float bulletSpeed = 10f;

    private void Start()
    {
        StartCoroutine(AutoFire());
    }

    private IEnumerator AutoFire()
    {
        while (true)
        {
            GameObject target = FindClosestTarget();
            if (target != null)
            {
                ShootAtTarget(target);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(currentPosition, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target;
            }
        }
        return closest;
    }

    private void ShootAtTarget(GameObject target)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Align bullet to face target
        spawnedBullet.transform.LookAt(target.transform.position);

        // Apply velocity to move forward
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = spawnedBullet.transform.forward * bulletSpeed;
        }

        // Destroy bullet after 5 seconds if it doesn't hit anything
        Destroy(spawnedBullet, 5f);
    }
}
