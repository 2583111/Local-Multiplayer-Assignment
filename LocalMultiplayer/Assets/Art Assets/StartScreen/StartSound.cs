using System.Collections;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    public AudioSource MonsterRoar;
    public GameObject Loader;

    public GameObject Monster;
    void Start()
    {
        StartCoroutine(SoundControl());
    }

    IEnumerator SoundControl()
    {
        yield return new WaitForSecondsRealtime(6);
        Monster.SetActive(true);
        MonsterRoar.Play();
        if (Loader.activeSelf == false)
        {
            StartCoroutine(Repeat());
        }
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSecondsRealtime(9);
        Monster.SetActive(false);
        if (Loader.activeSelf == false)
        {
            StartCoroutine(SoundControl());
        }
    }
}
