using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenControl : MonoBehaviour {

	Sprite currentToken;
	public Image token1;
	public Image token2;
	public Image token3;

	public GameObject tokensImage;
	public Button leftButton;
	public Button rightButton;

	public Image[] imageArray; //test to convert to array
	//public list<Sprite> tokensList = new list<Sprite> (); //idk aikaisempi testi
	//private int selectedImage; //dan teki tämän idk miten toimii täysin

	// Use this for initialization
	void Start () {
		//selectedImage = 0; //dan  again
		//currentToken = tokensImage.GetComponent<Sprite> ();

		imageArray = new Image[] {
			token1, token2, token3
		};

		  

		Button btn = leftButton.GetComponent<Button>();
		btn.onClick.AddListener (TaskOnClick);
	} 

	void TaskOnClick()
	{
		//selectedImage -= 1; //dan
		//currentToken = tokensList[selectedImage]; //dan
		//imageFrame.setImage (tokensList [selectedImage]); //dan
	}
	// Update is called once per frame
	void Update () {
	}
}


