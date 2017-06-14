using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour {
	public Sprite tokenSprite;
	TokenControl tokenControl;

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		tokenControl;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
