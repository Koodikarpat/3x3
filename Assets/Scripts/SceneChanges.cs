using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour {

	public Button localMultiplayer;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void ChangeSceneLocalMultiplayer ()
	{
		SceneManager.LoadScene ("scene");
	}
}
