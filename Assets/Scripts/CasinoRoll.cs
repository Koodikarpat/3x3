using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoRoll : MonoBehaviour {
    public float movementSpeed = 10;
    private float currentSpeed;
    private float speedtimer;
    // Use this for initialization
    void Start () {
        currentSpeed = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
        speedtimer += Time.deltaTime;
        if (movementSpeed - speedtimer > 0)
            currentSpeed = movementSpeed - speedtimer;

        
        
    }
}
