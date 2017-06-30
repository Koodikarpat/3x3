using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public List<GameObject> prefablist = new List<GameObject>();
    
    private float timer;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            int prefabIndex = UnityEngine.Random.Range(0, prefablist.Count);
            GameObject newpiece = Instantiate(prefablist[prefabIndex]);
            newpiece.AddComponent<Rigidbody2D>();
        }
		
	}
}
