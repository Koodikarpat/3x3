using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour {

	public enum TokenType
    {
        Candle = 0,
        Chest = 1,
        Inkbottle = 2,
        Key = 3,
        Knight = 4,
        Ring = 5,
        Teacup = 6,
        Teapot = 7,
        Crown = 8,
        Crystalball = 9,
        Sword = 10
    }
    public TokenType tokenType;

    public TokenType getTokenType()
    {
        return this.tokenType;
    }

}
