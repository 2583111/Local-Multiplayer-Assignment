using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Material Full_Mat;
    public Material Half_Mat;
    public Material Zero_Mat;

    public List<GameObject> Parts = new List<GameObject>();
    private Transform nS;

    private void Start()
    {
        nS = this.gameObject.transform.parent;
        CountChildren();
    }

    public void CheckDestruction()
    {
        CountChildren();

        if (Parts.Count == 0)
        {
            Debug.Log("Building destroyed");
            nS.GetComponent<NextScreen>().ClearFromList(this.gameObject);
            //Destroy(gameObject);
        }
    }

    private void CountChildren()  
    {
        Parts.Clear();

        foreach (Transform child in transform)
        {
            Parts.Add(child.gameObject);
        }
    }
}
