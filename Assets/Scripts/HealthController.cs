﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour 
{

	public int startingHealth = 20;
	public int currentHealth;
	public GameObject GameOverText;
	public GameObject healthCounterObject;
	public GameObject PoisonCounterObject;

	Text HealthCounter; 
	Text PoisonCounter;

	private bool GameOver;

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0) {
			currentHealth = 0;
			GameOver = true;
			GameOverText.GetComponent <Text> ().text = "Game Over!";
			Debug.Log ("Gameover");
		}
		PoisonCounter = PoisonCounterObject.GetComponent < Text > ();
		HealthCounter.text = "" + currentHealth;
		PoisonCounter.text = "" + GetComponent <StatusEffects> ().turnsLeft;
	
	}


	public void Heal (int amount)
	{

		currentHealth += amount;
		HealthCounter.text = "" + currentHealth;

	}


	// Use this for initialization
	void Start () 
	{
		//PoisonCounter = PoisonCounterObject.GetComponent < Text > ();
		HealthCounter = healthCounterObject.GetComponent < Text > ();
		GameOver = false;
	}
		
	// Update is called once per frame
	void Update () 
	{
	}
}