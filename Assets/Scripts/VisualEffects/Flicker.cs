using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour {

	private Image myImage;

	void Start(){
		myImage = GetComponent<Image> ();
	}

	void Update () {
		float x = Time.time;
		float darkness = Mathf.PerlinNoise(x,0);
		darkness = darkness / 2 +0.3f;
		myImage.color = new Color(darkness, darkness, darkness, 1f);
	}
}
  
