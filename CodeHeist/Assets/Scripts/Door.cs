using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableGameObject {

    public string objectName = "door1";

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
        GameState.loseCondition = GameState.LoseCondition.Door;
        lm.LoadLevel("LoseScreen");
    }

    public override string getObjectName()
    {
        return objectName;
    }

    public override string needsToBeNextToGameObjectName()
    {
        return "RobotPlayer";
    }

    public override IEnumerator executeCommand(string methodName)
    {
        switch (methodName)
        {
            case "open":
                OpenDoor();
                break;
            default:
                yield return new MethodExecutionResult(false, "Method not found: " + methodName);
                yield break;
        }

        yield return new MethodExecutionResult();
    }
    public void CloseDoor()
    {
        sp.sprite = closed;
        doorCollider.enabled = true;
    }
}
