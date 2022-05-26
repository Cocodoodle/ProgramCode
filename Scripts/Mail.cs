using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mail : MonoBehaviour
{
    public string title;
    public string message;
    private GameObject messageItem;
    private MessageManager messageManager;

    public AudioSource clickAudioSource;

    void Start()
    {
        messageItem = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        messageManager = GameObject.Find("GameManager").GetComponent<MessageManager>();
    }

    public void DisplayMessage()
    {
        messageManager.mailOpened = this.gameObject;

        clickAudioSource.Play();

        messageItem.SetActive(true);
        messageItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = title;
        messageItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = message;
    }
}
