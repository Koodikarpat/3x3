using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTokens : MonoBehaviour {

	public SpriteRenderer player1;
	public GameObject player2;

	public SpriteRenderer currentToken1; //what it is now, idk if needed with the other there?
	public static GameObject TokenSprite; //where sprite is
	public SpriteRenderer currentSprite; //the same? ^

	// Use this for initialization
	void Start () {
		
	}
	//get the part from other script?
	TokenControl tokenControl = TokenSprite.GetComponent<TokenControl> (); 

	/// currentToken1 


	// Update is called once per frame...
	void Update () 
	{
		
	}
	public void Player1TokenIs ()
	{
		player1 = currentSprite;
	}
}
