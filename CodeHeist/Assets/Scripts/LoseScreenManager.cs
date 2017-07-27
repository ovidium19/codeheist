using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenManager : MonoBehaviour {
    public  AudioClip[] clips;
    private AudioSource au;
    public Text textBlock;
	// Use this for initialization
	void Start () {
        au = GetComponent<AudioSource>();
      
        
        switch (GameState.loseCondition)
        {
            case GameState.LoseCondition.Door:
                au.clip = clips[2];
                textBlock.text = "You have to open the door before you reach it. Go to terminal to gain access.\n";
                break;
            case GameState.LoseCondition.Trap:
                au.clip = clips[1];
                textBlock.text = "You have to jump over trap using robot.Jump().";
                break;
            case GameState.LoseCondition.Wall:
                au.clip = clips[0];
                textBlock.text = "If you touch a wall, you turn on the alarms and get caught. Avoid touching walls";
                break;
            default:break;
        }
        Invoke("PlayVoiceOver", 3.0f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlayVoiceOver()
    {
       
        au.Play();
    }
}
