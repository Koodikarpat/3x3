using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTheseScript : MonoBehaviour { //renamed from SceneControllerScript, SceneChanges might also need renaming
	//permanent object = keeps the Object ObjectKeeper in all scenes - (TokenControl - SceneChange - (FindToken))

	//to move the player tokens to next scene:

	//token choosing script
	TokenControl tokenControl; 

	//player1 - new, works
	public GameObject currentToken;

	//player2 - new, writing
	public GameObject randomToken;

	//-------------------------------------

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
