using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public int startingHealth = 20;
	public int currentHealth;
	Text HealthCounter; 


	public void TakeDamage (int amount)
	{
			currentHealth -= amount;

		HealthCounter.text = ""+currentHealth;

		if (currentHealth <= 0) {
			Debug.Log("Game Over");

		}

		}

	// Use this for initialization
	void Start () {
		HealthCounter = GetComponent < Text > ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			TakeDamage (1);
		}



				
	}
}
