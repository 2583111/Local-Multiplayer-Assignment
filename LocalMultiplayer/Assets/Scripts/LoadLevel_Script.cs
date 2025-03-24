using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.Rendering.HDROutputUtils;

public class LoadLevel_Script : MonoBehaviour
{
    [SerializeField] Image loadingBar;
    public string NextScene;
    public float Timer;
    public GameObject Button;

    public AsyncOperation loadLevel;

    private void Start()
    {
        StartCoroutine(LoadNextLevel());
        Button.SetActive(false);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (loadingBar.fillAmount == 1.0f)
        {
            Button.SetActive(true);
        }
    }

    IEnumerator LoadNextLevel()
       {
           loadLevel = SceneManager.LoadSceneAsync(NextScene);

           loadLevel.allowSceneActivation = false;

           while (Timer != 9)
           {
            loadingBar.fillAmount = Mathf.Clamp01(Timer / .9f);
               yield return null;
           }
       }

    public void ContinueToScene()
    {
        loadLevel.allowSceneActivation = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
