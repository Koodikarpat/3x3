using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoScroll : MonoBehaviour
{
    public float movementSpeed = 1.2f;
    
    void Update()
    {
        if (transform.position.y < 300)
        {
            transform.Translate(Vector2.up * movementSpeed);
        }
    }
}
