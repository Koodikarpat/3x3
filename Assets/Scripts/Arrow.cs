using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Timeout = 1.75f;

    public Instantiator instantiator { private get; set; } // Set by instantiator in the same scene
    private float timeout;
    private GameObject currentObject;
    private bool rolling = false;

    void Update()
    {
        if (!rolling) // Skip update if done
            return;

        if (timeout > 0.0f)
            timeout -= Time.deltaTime;
        else if (timeout <= 0.0f) { // Timeout, get closest token
            float minDistance = Mathf.Infinity;
            CasinoRoll chosenRoll = null;
            CasinoRoll[] rolls = FindObjectsOfType(typeof(CasinoRoll)) as CasinoRoll[];
            foreach (CasinoRoll roll in rolls) { // Get the closest token and use that
                float distance = Vector3.Distance(roll.transform.position, transform.position);
                if (distance < minDistance) {
                    chosenRoll = roll;
                    minDistance = distance;
                }
            }
            if (chosenRoll.movementSpeed >= 0.1f) {
                timeout = 0.5f;
                return;
            }
            else {
                rolling = false;
                instantiator.SendMessage("ObjectStayed", chosenRoll);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!rolling) // Don't send objects accidentally after we are done
            return;

        if (collision.gameObject != currentObject && currentObject != null) // Check if a new object and current ref not null
            instantiator.SendMessage("ObjectPassed", currentObject); // Send latest object passed

        if (currentObject != collision.gameObject) {
            timeout = Timeout; // Object passed, reset timeout.
        }
        currentObject = collision.gameObject;
    }

    public void DoRoll()
    {
        timeout = Timeout;
        rolling = true;
    }
}
