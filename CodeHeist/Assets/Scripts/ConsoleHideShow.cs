using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleHideShow : MonoBehaviour {
    private GameObject console;
    private GameObject executeButton;
    private Text buttonText;
	// Use this for initialization
	void Start () {
        console = GameObject.Find("Console");
        executeButton = GameObject.Find("Execute");
        buttonText = transform.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void HideConsole()
    {
        foreach (RectTransform r in console.GetComponent<RectTransform>())
        {
            if (r.gameObject.name != "ShowButton")
            {
                r.gameObject.SetActive(false);
            }
        }
        buttonText.text = "Show Console";
        //console.SetActive(false);
        //executeButton.SetActive(false);
    }
    void ShowConsole()
    {
        buttonText.text = "Hide Console";
        foreach (RectTransform r in console.GetComponent<RectTransform>())
        {
            if (r.gameObject.name != "ShowButton")
            {
                r.gameObject.SetActive(true);
            }
        }
    }
    public void ExecuteButton()
    {
        if (buttonText.text == "Hide Console")
        {
            HideConsole();
        }
        else
        {
            ShowConsole();
        }
    }
}
