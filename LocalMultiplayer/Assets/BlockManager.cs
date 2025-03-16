using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform bM;
    public int hitCount = 5;

    public Mesh buildingState1;
    public Mesh buildingState2;
    public Mesh buildingState3;

    void Start()
    {
        bM = transform.parent;

        GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Full_Mat;
    }

    private void Update()
    {
        if (hitCount == 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            bM.GetComponent<BuildingManager>().CheckDestruction();
        }

        if (Time.time >= colliderWait)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    float colliderWait = 0;

    public void TakeDamage()
    {
        GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Taken damage");
        hitCount -= 1;

        if (Time.time > colliderWait)
        {
            colliderWait = Time.time + 0.5f;
        }

        if (hitCount == 3)
        {
            //gameObject.GetComponent<MeshFilter>().mesh = buildingState1;
            GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Half_Mat;
        }

        if (hitCount == 0)
        {
            GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Zero_Mat;
        }
    }

}
