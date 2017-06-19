using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffects : MonoBehaviour
{
	public GameObject tickPoison;
	public GameObject PoisonCounterObject;
	private int tl;

	public int turnsLeft {
		get {
			return tl;
		}
		set {
			PoisonCounter = PoisonCounterObject.GetComponent < Text > ();
			PoisonCounter.text = "" + value;

			tl = value;
		}
	}

	Text PoisonCounter;

	// Use this for initialization
	void Start (){
		turnsLeft = 0;
	}

	public void tick () {
		if (turnsLeft > 0) {
			tickPoison.GetComponent <Animator> ().SetTrigger ( "tickPoison");
			GetComponent <HealthController> ().TakeDamage (1);
			turnsLeft--;
		}
	}
		

}
