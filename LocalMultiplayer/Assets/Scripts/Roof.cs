using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour
{
    float nextTP = 0;
    public bool isTP = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge") && Time.time > nextTP)
        {
            other.transform.parent.position = new Vector3(other.transform.position.x, other.transform.position.y + 1, gameObject.transform.position.z - 1);
            other.gameObject.transform.parent.GetComponent<PlayerController>().isRoofed = true;

            GetComponent<BoxCollider>().isTrigger = false;
            nextTP = Time.time + 1f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().isTrigger = false;
            StartCoroutine(EnableRoofedAfterDelay(collision.gameObject.GetComponent<PlayerController>()));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine(DisableRoofedAfterDelay(collision.gameObject.GetComponent<PlayerController>()));
        }
    }

    private IEnumerator DisableRoofedAfterDelay(PlayerController player)
    {
        yield return new WaitForSeconds(1f);
        player.isRoofed = false;
    }

    private IEnumerator EnableRoofedAfterDelay(PlayerController player)
    {
        yield return new WaitForSeconds(1f);
        player.isRoofed = true;
    }

    private void Update()
    {
        if (Time.time >= nextTP)
        {
            isTP = false;
        }
    }
}
