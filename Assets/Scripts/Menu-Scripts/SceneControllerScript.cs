using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour { //might need renaming, this and SceneChanges
	//permanent object = keeps the Object SceneController in all scenes - (TokenControl - SceneChange - (FindToken))

	//to move the player tokens to next scene:
	//token choosing script
	TokenControl tokenControl; 

		//player1 (currentSprite = tokenSprite)
	//public Sprite tokenSprite; 

		//player1 - testing script
	public GameObject currentToken;

		//player 2: randomSprite = randomTokenSprite --UNDER CONSTRUCTION
	//public Sprite randomTokenSprite; --UNDER CONSTRUCTION

	//keep this object
	void Awake () {
		DontDestroyOnLoad(gameObject);
		//DontDestroyOnLoad (currentToken1);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
