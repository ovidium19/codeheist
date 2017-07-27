using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {
    const string MASTER_VOLUME_KEY = "Master Volume";
    const string DIFFICULTY_KEY = "Difficulty";

    public static void SetMasterVolume(float vol)
    {
        if (vol >= 0f && vol <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, vol);
        }
        else
        {
            Debug.LogError("Master volume out of range");
        }
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    public static void SetDifficulty(int diff)
    {
        if (diff>=1 && diff <= 3)
        {
            PlayerPrefs.SetInt(DIFFICULTY_KEY, diff);
        }
        else
        {
            Debug.LogError("Difficulty var not available");
        }
    }
    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
