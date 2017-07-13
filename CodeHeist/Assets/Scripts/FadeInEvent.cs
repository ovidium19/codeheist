using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEvent : MonoBehaviour {
    private Image im;
    private float alphaVar;
	// Use this for initialization
	void Start () {
        im = GetComponent<Image>();
        alphaVar = im.color.a;
	}
	
	// Update is called once per frame
	void Update () {
        alphaVar -= Time.deltaTime/2;
        im.color = new Color(im.color.r, im.color.g, im.color.g, alphaVar);
        if (im.color.a <= 0)
        {
            Destroy(gameObject);
        }
	}
}
