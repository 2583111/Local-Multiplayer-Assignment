using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform bM;
    public int hitCount = 5;

    void Start()
    {
        bM = transform.parent;

        GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Full_Mat;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitCount -=1;
        }
    }

    private void Update()
    {
        if (hitCount == 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            bM.GetComponent<BuildingManager>().CheckDestruction();
        }
    }

}
