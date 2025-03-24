using System.Collections;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    public AudioSource MonsterRoar;

    public GameObject Monster;
    void Start()
    {
        StartCoroutine(SoundControl());
    }

    IEnumerator SoundControl()
    {
        yield return new WaitForSecondsRealtime(10);
        Monster.SetActive(true);
        MonsterRoar.Play();

            StartCoroutine(Repeat());
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSecondsRealtime(9);
        Monster.SetActive(false);

            StartCoroutine(SoundControl());       
    }
}
