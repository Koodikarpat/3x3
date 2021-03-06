﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour 
{
	public GameObject buttonGroup;
    //Keeping track of the location of the player gameobject; "what button is the player on at the moment"
    public int currentButton;
    public int lastButton;
	public GameObject turnControlObject;
	public GameObject enemy;
    public GameObject puff;
	public GameObject multiplayerController;

    static float t = 0.0f;

    public bool serverAnswered = true;
	public bool animationsFinished = true;

    public bool canMove = false;

    ButtonSelection buttons;
    TilePlacements tilePlacements;
	Multiplayer multiplayer;

	void Start () 
	{
		buttons = buttonGroup.GetComponent<ButtonSelection> ();
        tilePlacements = buttonGroup.GetComponent<TilePlacements> ();
		multiplayer = multiplayerController.GetComponent<Multiplayer>();

		//startin locations
		//Debug.Log (transform.name + " " + buttons.tiles[currentButton].position);
		transform.position = buttons.tiles[currentButton].gameObject.transform.position;
        lastButton = currentButton;

        canMove = true;
    }

	void Update () 
	{

	}

	public void MoveButton(int button, bool remote = false)
	{
        // the movement itself
        // move must always be legal, if online game it must also be your turn
		if (isLegalMove (currentButton, button) && canMove)
        {

			// if online game
			if (multiplayer.isOnline && !remote) {

				// make online move
				multiplayer.MovePiece (button);
				serverAnswered = false;
			}
			
		    animationsFinished = false;
			StartCoroutine (waitAnimations (button));
		} else {
			Debug.Log ("Someone tried to do an illegal move" + " start: " + currentButton + " end: " + button);
		}
	}

    public void ChangeTile(int type, int strength)
    {
        buttons.tiles[lastButton].type = tilePlacements.GetEffect(type, strength);
        tilePlacements.CreateTile(buttons.tiles[lastButton], lastButton);
        lastButton = currentButton;
    }

	private IEnumerator waitAnimations(int button)
	{
        t = 0;
		while (animationsFinished == false)
		{
            canMove = false;
            transform.position = Vector3.Lerp(buttons.tiles[currentButton].position, buttons.tiles[button].position, t);
            t += 1.5f * Time.deltaTime;
            
                if ((Vector2)transform.position == buttons.tiles[button].position)

                animationsFinished = true;

            yield return null;
		}
        canMove = true;
        finishMove (button);
    }

	private void finishMove(int button)
	{
	    TurnControl turncontrol = turnControlObject.GetComponent<TurnControl> ();

        if (!multiplayer.isOnline) {
            turncontrol.ChangeTurn();
            StartCoroutine(buttons.CreateTile(currentButton));
        }

        currentButton = button;

        buttons.tiles [currentButton].type.Action (gameObject, enemy);
	    Animator Animator = buttons.tiles [currentButton].gameObject.GetComponentInChildren<Animator> ();
	    Animator.SetTrigger ("Step on");

        if (puff != null) {
            puff.GetComponent<ParticleSystem>().Play();
            puff.transform.position = buttons.tiles[button].position;
        }
    }


    bool isLegalMove(int start, int end) // restricting player movements to just one button away and only horizontally and vertically.
    {
        if (end == enemy.GetComponent<PlayerAbilities>().currentButton)
            return false;

        if ((start - 3 == end) || (start + 3 == end))
            return true;

        if (start + 1 == end)
        {
            if ((start == 2) || (start == 5))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        if (start - 1 == end)
        {
            if ((start == 3) || (start == 6))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
        //TODO STILL NEED TO RESTRICT THEM FROM OVERLAPPING!!
    }
}
