using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Transform bM;

    void Start()
    {
        bM = transform.parent;

        GetComponent<MeshRenderer>().material = bM.GetComponent<BuildingManager>().Full_Mat;
    }


    void Update()
    {
        
    }
}
