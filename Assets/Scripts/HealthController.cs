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
	public GameObject PoisonCounterObject;
	public GameObject animator;

	Text HealthCounter; 
	Text PoisonCounter;

	private bool GameOver;

    public void TakeDamage(int amount)
    {
        animator.GetComponent<Animator>().SetTrigger("TakeDamage");
        currentHealth -= amount;
        checkHealth(currentHealth);
		HealthCounter.text = "" + currentHealth;
    }

	public void Heal (int amount)
	{
		currentHealth += amount;
		HealthCounter.text = "" + currentHealth;
	}

	// Use this for initialization
	void Start () 
	{
		HealthCounter = healthCounterObject.GetComponent < Text > ();
		GameOver = false;
	}

    public void checkHealth(int health)
    {
        if (health <= 0)
        {
            currentHealth = 0;
            GameOver = true;
            GameOverText.GetComponent<Text>().text = "Game Over!";
            Debug.Log("Gameover");
        }

    }
		
	// Update is called once per frame
	void Update () 
	{
	}
}