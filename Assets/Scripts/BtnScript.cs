using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScript : MonoBehaviour
{
    AudioSource se;
    private bool canSound = true;
    private float waitTime = 1.0f;
    void Start()
    {
        se = GetComponent<AudioSource>();
    }

    public void ClickVSBottom()
    {
        SceneManager.LoadScene("ModeVs");
    }

    public void ClickSingleBottom()
    {
        SceneManager.LoadScene("ModeSingle");
    }

    public void ClickTitleBottom()
    {
        SceneManager.LoadScene("Title");
    }

    public void PlayVS()
    {
        if(canSound)
        {
            GetComponent<AudioSource>().PlayOneShot(se.clip);
            StartCoroutine("LoadVS");
        }
        canSound = false;
    }
    public void PlaySingle()
    {
        if(canSound)
        {
            GetComponent<AudioSource>().PlayOneShot(se.clip);
            StartCoroutine("LoadSingle");
        }
        canSound = false;
    }
    public void ToTitle()
    {
        if(canSound)
        {
            GetComponent<AudioSource>().PlayOneShot(se.clip);
            StartCoroutine("LoadTitle");
        }
        canSound = false;
    }

    IEnumerator LoadVS()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("ModeVs");
    }
    IEnumerator LoadSingle()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("ModeSingle");
    }
    IEnumerator LoadTitle()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Title");
    }
}
