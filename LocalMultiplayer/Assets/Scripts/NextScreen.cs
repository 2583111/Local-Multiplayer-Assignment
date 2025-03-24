using System.Collections;
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

    public BoxCollider divider;

    private GameObject goUI;

    private void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            allBuildings.Add(child.gameObject);
        }

        goUI = GameObject.Find("GameManager").GetComponent<GameManager>().goUI;
        goUI.SetActive(false); // Ensure UI is initially disabled
    }

    private void Update()
    {
        if (isCleared)
        {
            StartCoroutine(ShowGoUI());
        }
    }

    private void ClearanceCheck()
    {
        if (allBuildings.Count < 1)
        {
            Debug.Log("Go");
            isCleared = true;
           

        }
    }

    private IEnumerator ShowGoUI()
    {
        goUI.SetActive(true);
        yield return new WaitForSeconds(3f); // Show for 3 seconds
        goUI.SetActive(false);
    }

    public void ClearFromList(GameObject that)
    {
        Debug.Log("Removed");
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

                if (canSpawnSoldiers)
                {
                    Debug.Log("spawned");
                    newSoldiers = Instantiate(soldierSet, soldierSpawn);
                }

                divider.enabled = false;
            }
            else if (isCleared && goBack == true)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ShiftBack();
                goBack = false;
            }
        }
    }
}
