using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Connector : MonoBehaviour
{
    public Camera cam;
    public Main main;

    Vector2 mousePos;

    private void Start()
    {
        cam = Camera.main;
        main = GameObject.Find("ProgramTextsAndButtons").transform.GetChild(3).GetComponent<Main>();
    }

    void Update()
    {
        if (main.startConnector && main.GetConnector() == this.gameObject)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 diff = mousePos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            transform.localScale = new Vector3(diff.magnitude, transform.localScale.y, 0);
        }
    }
}
