using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MethodsText : MonoBehaviour {
    public Text buttonText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HideCanvas()
    {
        foreach (Transform t in transform)
        {
            if (!t.gameObject.GetComponent<Button>())
            {
                t.gameObject.SetActive(false);
            }
           
        }
        buttonText.text = "Show";
    }
    public void ShowCanvas()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
        buttonText.text = "Close";
    }
    public void ExecuteButton()
    {
        if (buttonText.text == "Show")
        {
            ShowCanvas();
        }
        else
        {
            HideCanvas();
        }
    }
}
