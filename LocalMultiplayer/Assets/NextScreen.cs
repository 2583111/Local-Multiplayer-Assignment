using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NextScreen : MonoBehaviour
{

    public List<GameObject> allBuildings = new List<GameObject>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (isCleared && goBack == false)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ShiftNext();
            goBack = true;
           
        }

        if (isCleared && goBack == true)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ShiftBack();
            goBack = false;
        }
    }

}
