using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NextScreen : MonoBehaviour
{

    public List<GameObject> allBuildings = new List<GameObject>();
    public bool canSpawnSoldiers = false;
    public GameObject soldierSet;
    public Transform soldierSpawn;

    public bool isCleared;
    public bool goBack = false;

    private void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            allBuildings.Add(child.gameObject);
        }
    }

    private void ClearanceCheck()
    {
        if (allBuildings.Count == 0)
        {
            isCleared = true;
            
        }
    }

    public void ClearFromList(GameObject that)
    {
        allBuildings.Remove(that);

        ClearanceCheck();
    }

    private GameObject newSoldiers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (isCleared && goBack == false)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ShiftNext();
                goBack = true;
                canSpawnSoldiers = true;
            }

            if (canSpawnSoldiers)
            {
                Debug.Log("spawned");
                newSoldiers = Instantiate(soldierSet, soldierSpawn);
            }

            else if (isCleared && goBack == true)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ShiftBack();
                goBack = false;

                if (canSpawnSoldiers)
                {
                    //Destroy(newSoldiers);
                }

            }
            
        }

    }

}
