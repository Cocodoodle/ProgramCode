using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramConnector : MonoBehaviour
{
    private Main main;
    public GameObject snapPoint;
    private GameObject connector;
    public ProgramManager programManager;

    public AudioSource clickAudioSource;
    public AudioSource detrachAudioSource;

    private void Start()
    {
        main = GameObject.Find("ProgramTextsAndButtons").transform.GetChild(3).GetComponent<Main>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && main.startConnector)
        {
            AttachConnector();
        }
    }

    public void AttachConnector()
    {
        if(connector != null) { return; }

        connector = main.GetConnector();

        if(main.GetConnector() == null)
        {
            GameObject newConnector = GameObject.Instantiate(main.connectorPrefab, main.gameObject.transform.position, Quaternion.identity);
            main.SetConnector(newConnector);
            connector = newConnector;
        }

        Vector3 diff = snapPoint.transform.position - connector.transform.position;
        main.startConnector = false;
        connector.transform.localScale = new Vector3(diff.magnitude, connector.transform.localScale.y, 0f);

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        connector.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        clickAudioSource.Play();

        programManager.program.isUsed = true;

        main.connectorAvailable = false;
        main.SetConnector(null);
    }

    public void DetachConnector()
    {
        programManager.program.isUsed = false;

        detrachAudioSource.Play();

        if(connector != null)
        {
            Destroy(connector);
        }
    }
}
