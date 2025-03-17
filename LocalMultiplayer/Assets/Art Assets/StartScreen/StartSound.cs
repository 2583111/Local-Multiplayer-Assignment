using System.Collections;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    public AudioSource MonsterRoar;
    public GameObject Loader;
    void Start()
    {
        StartCoroutine(SoundControl());
    }

    IEnumerator SoundControl()
    {
        yield return new WaitForSecondsRealtime(6);
        MonsterRoar.Play();
        if (Loader == null)
        {
            StartCoroutine(Repeat());
        }
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSecondsRealtime(9);
        if (Loader == null)
        {
            StartCoroutine(SoundControl());
        }
    }
}
