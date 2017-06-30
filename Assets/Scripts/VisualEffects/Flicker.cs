using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour {

	public Image myImage;

	void Start(){
		myImage = GetComponent<Image> ();
	}

	void Update () {
		float x = Time.time;
		float darkness = Mathf.PerlinNoise(x,0);
		darkness = darkness / 2 +0.3f;
		myImage.color = new Color(darkness, darkness, darkness, 1f);
		//myImage.CrossFadeAlpha (1.0f, 1.0f, true);//CrossFadeAlpha(Alpha, Time, Ignore time scale)
	}
}
  
