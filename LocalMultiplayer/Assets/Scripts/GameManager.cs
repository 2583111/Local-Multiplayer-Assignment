using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> levelColliders = new List<GameObject>();
    public List<Transform> cameraPositions = new List<Transform>();

    private GameObject MainCamera;
    public int currentScreen = 0;

    public GameObject goUI;

    public GameObject p1;
    public GameObject p2;

    public GameObject WinScreenP1;
    public GameObject WinScreenP2;

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

    public void CheckWinner()
    {
        float Score1 = p1.GetComponent<PlayerController>().playerScore;
        float Score2 = p2.GetComponent<PlayerController>().playerScore;

        if (Score1 > Score2)
        {
            Debug.Log("Player 1 wins with a score of: " + Score1);
            WinScreenP1.SetActive(true);
        }
        else if (Score2 > Score1)
        {
            Debug.Log("Player 2 wins with a score of: " + Score2);
            WinScreenP2.SetActive(true);
        }
        else
        {
            Debug.Log("It's a tie! Both players have a score of: " + Score1);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
