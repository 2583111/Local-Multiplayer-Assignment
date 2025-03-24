using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> levelColliders = new List<GameObject>();
    public List<Transform> cameraPositions = new List<Transform>();

    private GameObject MainCamera;
    public int currentScreen = 0;

    public GameObject goUI;


    private void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        MainCamera.transform.position = cameraPositions[0].position;

    }

    public void ShiftNext()
    {
        currentScreen++;
    }

    public void ShiftBack()
    {
        currentScreen--;
    }

    public void Update()
    {

        MainCamera.transform.position = cameraPositions[currentScreen].position;

    }
}
