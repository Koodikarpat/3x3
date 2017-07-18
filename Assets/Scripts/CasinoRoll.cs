using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoRoll : MonoBehaviour {
    public float movementSpeed;
	
	// Update is called once per frame
	void Update () {
        if (movementSpeed > 0) {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
    }

    private void DiscardMe()
    {
        Destroy(this.gameObject); // TODO: Discard effect
    }
}
