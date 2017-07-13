using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject defender;
    public GUISkin skin;
    public static GameObject selectedDefender;
    private GameObject[] buttons=new GameObject[4];
    private bool hovering = false;
	// Use this for initialization
	void Start () {
        int i = 0;
        Transform t = GameObject.Find("Buttons").transform;
        foreach (Transform x in t)
        {
            if (x.name != "Background")
            {
                buttons[i] = x.gameObject;
                i++;
            }
        }
       // int randomIndex = (int)(Random.Range(0,3.1f));
       // Debug.Log(randomIndex);

       // buttons[randomIndex].GetComponent<Button>().SelectDefender();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*void OnGUI()
    {
        if (hovering)
        {
            GUI.Box(new Rect(Input.mousePosition.x, Input.mousePosition.y-1000, 200, 200),
           defender.GetComponent<Defender>().price.ToString(), skin.GetStyle("buttonTooltip"));
        }
       
    }*/
    void OnMouseDown()
    {
        SelectDefender();
    }
    void OnMouseOver()
    {
        hovering = true;
    }
    void OnMouseExit()
    {
        hovering = false;
    }
    void SelectDefender()
    {
        selectedDefender = defender;

        GetComponent<SpriteRenderer>().color = Color.white;
        foreach (GameObject g in buttons)
        {
            if (g.name != name)
            {
                g.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }
}
