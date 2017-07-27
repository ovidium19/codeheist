using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {
    public Door terminalAccessDoor;//TODO remove unused
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<Robot>().AddObject(gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<Robot>().RemoveObject(gameObject);
    }
}
