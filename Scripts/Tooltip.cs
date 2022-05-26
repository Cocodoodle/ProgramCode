using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public GameObject toolTip;
    public TextMeshProUGUI title;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI benifit;
    public TextMeshProUGUI factNum;

    public ProgramManager programManager;

    private float timer;
    public float timeTillShow;

    public Camera cam;

    public void Update()
    {
        toolTip.transform.position = cam.ViewportToScreenPoint(Input.mousePosition);
    }

    private void OnMouseOver()
    {
       toolTip.SetActive(true);
    }

    private void OnMouseExit()
    {
        toolTip.SetActive(false);
    }
}
