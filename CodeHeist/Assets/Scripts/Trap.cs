using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour {
    private LevelManager lm;
	// Use this for initialization
	void Start () {
        lm = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        GameState.loseCondition = GameState.LoseCondition.Trap;
        lm.LoadLevel("LoseScreen");
    }
}
