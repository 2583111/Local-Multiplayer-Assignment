using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
    public GameObject PauseScreen;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            PauseScreen.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseScreen.SetActive(false);
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("S.Scene");
    }
}
