using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public float movementSpeed = 3;
    public TextAsset text;

    void Start()
    {
        if(text != null)
            GetComponent<Text>().text = text.text;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector2.up * movementSpeed);
	}
}
