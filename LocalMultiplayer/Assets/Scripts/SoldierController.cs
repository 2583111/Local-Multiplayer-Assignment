using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public string targetTag = "Play"; // Change this to whatever tag your target has
    public float fireRate = 2f; // Time between shots
    public float bulletSpeed = 10f; // Speed of bullet movement

    public float defaultScore = 20;

    public GameObject bloodParticles;

    private GameObject player1;
    private GameObject player2;

    private void Start()
    {
        StartCoroutine(AutoFire());

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
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
        GameObject spawnedBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.transform.rotation);
        StartCoroutine(MoveBulletToTarget(spawnedBullet, target.transform));
    }

    private IEnumerator MoveBulletToTarget(GameObject bullet, Transform target)
    {
        while (bullet != null && target != null)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, bulletSpeed * Time.deltaTime);
            if (Vector3.Distance(bullet.transform.position, target.position) < 0.1f)
            {
                Destroy(bullet);
                break;
            }
            yield return null;
        }
    }

    public void Die(int thisPlayer)
    {

        Instantiate(bloodParticles, gameObject.transform.position, gameObject.transform.rotation);

        if (thisPlayer == 0)
        {
            PlayerController p1 = player1.GetComponent<PlayerController>();
            p1.playerScore += defaultScore;
            p1.updateScore();
        }

        else
        {
            PlayerController p2 = player2.GetComponent<PlayerController>();
            p2.playerScore += defaultScore;
            p2.updateScore();
        }

        Destroy(gameObject);

    }

}
