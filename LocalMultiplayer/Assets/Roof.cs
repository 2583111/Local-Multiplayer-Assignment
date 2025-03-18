using UnityEngine;

public class Roof : MonoBehaviour
{
    float nextTP = 0;
    public bool isTP = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge") && Time.time > nextTP)
        {
            Debug.Log("works");
            other.transform.parent.position = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);

            //isTP = true;
            GetComponent<BoxCollider>().isTrigger = false;
            nextTP = Time.time + 1f;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void Update()
    {
        if (Time.time >= nextTP)
        {
            isTP = false;
        }

       /* if (isTP == true)
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }


        if (isTP == false)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }*/

    }

}
