using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class LoadLevel_Script : MonoBehaviour
{
    [SerializeField] Image loadingBar;

    private void Start()
    {
        StartCoroutine(LoadNextLevel ());
    }

    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("M_Scene");

        while (!loadLevel.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(loadLevel.progress/ .9f);
            yield return null;
        }
    }
}
