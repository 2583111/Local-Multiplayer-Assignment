using UnityEngine;

public class Bullet : MonoBehaviour
{

    /*private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(0.5f);

        }

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Found");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(0.5f);
        }
    }

}
