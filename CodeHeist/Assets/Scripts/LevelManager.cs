using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public AudioClip loadingSound;

    private int splashScreen;

	// Use this for initialization
	void Start () {
        splashScreen = SceneManager.GetActiveScene().buildIndex;
        if (splashScreen == 0)
        {
            PlaySplashScreen();
        }
        
	}
    void PlaySplashScreen()
    {
        AudioSource.PlayClipAtPoint(loadingSound, transform.position);
        
        Invoke("LoadNextLevel", Random.Range(3f, 6f));
    }
    public  void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ButtonClicked(string button)
    {
        Debug.Log("Clicked " + button);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
