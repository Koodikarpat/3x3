using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public List<GameObject> prefablist = new List<GameObject>();
    
    private float timer;
    private float speedtimer;

    public float startSpeed;
    // Use this for initialization
    void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        speedtimer += Time.deltaTime;
        Debug.Log(10 / speedtimer);
        if (timer > speedtimer / 20)
        {
            timer = 0;
            if (startSpeed - speedtimer > 0)
            {
                int prefabIndex = UnityEngine.Random.Range(0, prefablist.Count);
                GameObject newpiece = Instantiate(prefablist[prefabIndex], transform);
                CasinoRoll casinoRoll = newpiece.AddComponent<CasinoRoll>();
                casinoRoll.movementSpeed = startSpeed - speedtimer;
            }

            
        }
		
	}
}
