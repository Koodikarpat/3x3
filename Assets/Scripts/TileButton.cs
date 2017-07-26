using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public int buttonnumber;
    ButtonSelection buttonSelection;

    void Start()
    {
       GameObject TileGroup = GameObject.Find("TileGroup");
       buttonSelection = TileGroup.GetComponent<ButtonSelection>();
    }



    void OnMouseDown()
    {
        buttonSelection.click(buttonnumber);
    }
}