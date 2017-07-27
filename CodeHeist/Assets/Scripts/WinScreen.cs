using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {
    private AudioSource au;
	// Use this for initialization
	void Start () {
        au = GetComponent<AudioSource>();
        Invoke("PlayVoiceOver", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlayVoiceOver()
    {
        au.Play();
    }
}
