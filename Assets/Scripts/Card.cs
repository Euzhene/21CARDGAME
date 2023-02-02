using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Card
{
    public Sprite image;
    public int value{get;private set;}
    
    public Card(int value, Sprite image) {
        CARD card = (CARD)value;
        this.value = (int)Enum.Parse(typeof(VALUECARD), card.ToString());
        this.image = image;
    }

    public enum VALUECARD
    {
        JACKET = 2, LADY = 3, KING = 4, ACE = 11, SIX = 6, 
        SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10
    }
    public enum CARD
    {
        JACKET, LADY, KING, ACE, SIX,
        SEVEN, EIGHT, NINE, TEN
    }
}
