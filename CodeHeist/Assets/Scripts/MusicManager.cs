using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
    public AudioClip[] music;
  
    private AudioSource au;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        au = GetComponent<AudioSource>();
        au.volume = PlayerPrefsManager.GetMasterVolume();
    }
	// Use this for initialization
	void Start () {
        
        //au.Stop();
	}
	
    void OnLevelWasLoaded(int level)
    {
        if (level < music.Length && music[level])
        {
            if (level==1 && au.isPlaying) { return; }
            Debug.Log("Playing : " + music[level].ToString());
            au.clip = music[level];
            

            au.Play();
        }
    }
    public void ChangeVolume(float value)
    {
        if (value>=0f && value <= 1f)
        {
            au.volume = value;
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
