using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Networking;
using System.Threading;

public class SceneChanges : MonoBehaviour { //might need renaming, this. KeepTheseScriptScript = SceneControllerScript, renamed to something more fitting.
	//connected to - Buttons, will include all the scene change button functions

	//button objects
	public Button localMultiplayer;
	public Button Online;
    public Button credits;
    public Button backToMainMenu;
    public Button changeUsername;

	//transition camera
	Animator animator;
	CanvasRenderer renderer;

	//for moving the player tokens (KeepTheseScriptScript, (TokenControl script))
	public GameObject ObjectKeeper;
	public GameObject CanvasAuki;
	public GameObject Canvas;
	public GameObject Canvashuone;

	//player1 - new, works again
	public GameObject TokenControl;
    public string player1Name;

	//player2 random - new, MOVED FROM TOKENCONTROL
	public GameObject randomToken;
	int randomPrefabIndex;
	public GameObject[] prefabArray;

	// Use this for initialization
	void Start () {

        if(CanvasAuki != null)
		    animator = CanvasAuki.GetComponent<Animator> ();

	}

	// Update is called once per frame.
	void Update () {



		
	}

	//Transition animation
	IEnumerator SceneTransition()
	{
		
		CanvasAuki.SetActive (true);
        Debug.Log("setting trigger");
        animator.SetTrigger ("Hide");
		Canvashuone.SetActive (true);
		yield return new WaitForSeconds(1);
		//SceneManager.LoadScene ("miikan scene");



	}

	public void ChangeSceneLocalMultiplayer () //go to play scene (local multiplayer)
	{
		

		//move the right token to the play scene
		KeepTheseScript keepTheseScript = ObjectKeeper.GetComponent<KeepTheseScript> ();//get script

		//----------------------------------------------------------------------------------

			//player1 - works again, new

		Vector3 currentPosition = TokenControl.GetComponent<TokenControl>().currentToken.transform.localPosition;
		TokenControl.GetComponent<TokenControl>().currentToken.transform.parent = ObjectKeeper.transform; 
		keepTheseScript.currentToken = TokenControl.GetComponent<TokenControl>().currentToken;
	
		//because the shade-cursed thing moves. doesn't work completely yet

		keepTheseScript.currentToken.transform.localPosition = currentPosition;

		//-----------------------------------------------------------------------------

		//player2 random -new, MOVING THIS TO SCENECHANGES lets see what breaks
		TokenControl tokenControl = TokenControl.GetComponent<TokenControl> (); //get script
		prefabArray = tokenControl.prefabArray;

		//randomizing, shouldn't be same as 1 but atm is. FIX
		randomPrefabIndex = Random.Range (0, prefabArray.Length);
		if (randomPrefabIndex == tokenControl.tokenCounter)
				randomPrefabIndex++;
		if (randomPrefabIndex > prefabArray.Length - 1)
			randomPrefabIndex = 0;
		randomToken = SetPrefab2 (randomPrefabIndex);
				

		//player2 -DO NOT TOUCH, it works
		Vector3 localPositionNow = randomToken.transform.localPosition; //NEW
		randomToken.transform.parent = keepTheseScript.transform; //PARENT = ObjectKeeper
		keepTheseScript.randomToken = randomToken;
		//because the shade-cursed thing moves. doesn't work completely yet
		randomToken.transform.position = localPositionNow; //NEW

		//------------------------------------------------------------------------------------

		//Hiding the buttons

		GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Piilotettava");

		foreach(GameObject go in gameObjectArray)
		{
			go.SetActive (false);
		}

		//playing the transition animation and changing the scene
		StartCoroutine(SceneTransition());

	}
	public void ChangeSceneOnline () //loading Screen for Online version
	{
		// TODO: initialize the pieces
		// TODO: initialize Multiplayer Controller object with multiplayer.isOnline = true
		SceneManager.LoadScene("reetun scene"); //TODO: there should be a lobby/waiting for game scene or the mirkan scene should have a "waiting for game" dialog

	}

    public void ChangeSceneCredits()
    {
        SceneManager.LoadScene ("Credits");
    }

    public void ChangeSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu_Aleksi");
    }

    public void openUsernamePanel()
    {
        gameObject.SetActive(false);
    }

    public void closeUsernamePanel()
    {
        gameObject.SetActive(true);
    }


    //player2 - makes an object out of the prefab
    GameObject SetPrefab2 (int randomPrefabIndex) //IT WORKS
	{
		Destroy (randomToken);
		return Instantiate (prefabArray [randomPrefabIndex], transform);
	}
}
