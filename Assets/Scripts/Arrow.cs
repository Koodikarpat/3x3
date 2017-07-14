using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("teksti.");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("teksti 2.");
    }
}
