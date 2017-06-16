using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour {
	//keeps the Object SceneController in all scenes - (TokenControl - SceneChange - (FindToken))

	//to move the player tokens to next scene:
	//token choosing script
	TokenControl tokenControl; 
		//player 1: currentSprite = tokenSprite
	//public Sprite tokenSprite;
		//player 2: randomSprite = randomTokenSprite
	//public Sprite randomTokenSprite; 

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
