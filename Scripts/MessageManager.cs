using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject mailPrefab;
    public GameObject mailParent;
    public GameObject mail;

    public GameObject mailOpened;

    public AudioSource exitAudioSource;
    public AudioSource sentAudio;

    private void Update()
    {
        if(mailParent.transform.childCount > 7)
        {
            Destroy(mailParent.transform.GetChild(0).gameObject);
        }
    }

    public void CreateMail(string tilte, string message)
    {
        sentAudio.Play();

        GameObject mail = GameObject.Instantiate(mailPrefab, mailParent.transform);
        mail.GetComponent<Mail>().message = message;
        mail.GetComponent<Mail>().title = tilte;
    }

    public void CloseMail()
    {
        exitAudioSource.Play();

        mail.SetActive(false);

        if (mailOpened != null)
        {
            Destroy(mailOpened);
        }
    }
}
