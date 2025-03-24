using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(5);

        }

    }

}
