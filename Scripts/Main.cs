using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameObject connector;
    public GameObject connectorPrefab;
    public bool connectorAvailable = false;
    public bool startConnector = false;

    public AudioSource clickAudioSource;
    public AudioSource cancelAudioSource;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && startConnector)
        {
            startConnector = false;
            connector.transform.localScale = new Vector3(0, connector.transform.localScale.y, 0);
            cancelAudioSource.Play();
        }
    }

    public GameObject GetConnector()
    {
        return connector;
    }

    public void SetConnector(GameObject newConnector)
    {
        connector = newConnector;
    }

    public void UseConnector()
    {
        if (connectorAvailable == false)
        {
            connector = GameObject.Instantiate(connectorPrefab, transform.position, Quaternion.identity);
            connectorAvailable = true;
        }

        clickAudioSource.Play();
        startConnector = true;
    }
}

