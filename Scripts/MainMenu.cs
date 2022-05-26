using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;

    public float flashTime;
    public float notFlashingTime;

    public Animator animator;

    public int s_Index;

    public AudioSource AudioSource;

    void Start()
    {
        //StartCoroutine(Flashing());
    }

    public IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(notFlashingTime);
            startButton.SetActive(false);
            yield return new WaitForSeconds(flashTime);
            startButton.SetActive(true);
        }
    }

    public void PlayGame()
    {
        AudioSource.Play();
        StartCoroutine(LoadLevel());
        SceneManager.LoadScene(s_Index);
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(s_Index);
    }

}
