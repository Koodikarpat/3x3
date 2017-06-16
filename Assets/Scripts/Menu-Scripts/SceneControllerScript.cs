using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour {//keeps the Object SceneController in all scenes
	//to move the player tokens
	public Sprite randomTokenSprite; //player 2: test

	public Sprite tokenSprite; //player 1: currentSprite = tokenSprite
	TokenControl tokenControl; //token choosing script

	//keep this object
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
