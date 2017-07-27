using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorConsole : MonoBehaviour {
    private Text textBox;
    void Start()
    {
        textBox = transform.GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }
	public void ShowConsole(string message)
    {
        gameObject.SetActive(true);
        textBox.text = message;

    }
    public void HideConsole()
    {
        gameObject.SetActive(false);
    }
}
