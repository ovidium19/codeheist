using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public Sprite closed;
    public Sprite open;
    private BoxCollider2D doorCollider;
    private SpriteRenderer sp;
    private LevelManager lm;
	// Use this for initialization
	void Start () {
        doorCollider = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        lm = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenDoor()
    {
        sp.sprite = open;
        doorCollider.enabled = false;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        lm.LoadLevel("LoseScreen");
    }
}
