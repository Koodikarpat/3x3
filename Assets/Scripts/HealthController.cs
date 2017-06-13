using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour 
{

	public int startingHealth = 20;
	public int currentHealth;
	public GameObject GameOverText;
	public GameObject healthCounterObject;

	Text HealthCounter; 

	private bool GameOver;

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			GameOver = true;
			GameOverText.GetComponent <Text> ().text = "Game Over!";
		}

		HealthCounter.text = "" + currentHealth;
	}


	public void Heal (int amount)
	{

		currentHealth += amount;

	}


	// Use this for initialization
	void Start () 
	{
		HealthCounter = healthCounterObject.GetComponent < Text > ();
		GameOver = false;
	}
		
	// Update is called once per frame
	void Update () 
	{

	}
}