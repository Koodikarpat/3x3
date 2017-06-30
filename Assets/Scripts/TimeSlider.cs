using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour {

    // Use this for initialization
    void Start () {

        Slider mySlider = GetComponent<Slider>();
        float value = 1f;
        mySlider.value = value;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
