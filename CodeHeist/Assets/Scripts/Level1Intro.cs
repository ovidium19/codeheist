using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Intro : MonoBehaviour {
    public AudioClip[] clips;
    private int index;
    private AudioSource au;
	// Use this for initialization
	void Start () {
        au = GetComponent<AudioSource>();
        index = 0;
        au.clip = clips[index];
        au.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (!au.isPlaying)
        {
            index += 1;
            if (index< clips.Length)
            {
                au.clip = clips[index];
                au.Play();
            }
        }
	}
}
