using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
    public Slider volumeSlider;
    
    public LevelManager lm;

    private MusicManager music;
	// Use this for initialization
	void Start () {
        music = GameObject.FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        
	}
	
	// Update is called once per frame
	public void SetVolume()
    {
        music.ChangeVolume(volumeSlider.value);
    }
    public void SaveAndExit()
    {
        music.ChangeVolume(volumeSlider.value);
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        
        lm.LoadLevel("Start");
    }
}
