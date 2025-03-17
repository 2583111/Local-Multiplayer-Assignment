using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Material Full_Mat;
    public Material Half_Mat;
    public Material Zero_Mat;

    public List<GameObject> Parts = new List<GameObject>();

    private void Start()
    {
        CountChildren();
    }

    public void CheckDestruction()
    {
        CountChildren();

        if (Parts.Count == 0)
        {
            Debug.Log("Building destroyed");
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
