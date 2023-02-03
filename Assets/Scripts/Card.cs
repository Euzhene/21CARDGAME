using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Sprite image { get; private set; }
    public int value { get; private set; }

    public Card(int value, Sprite image)
    {
        CardName cardName = (CardName)value;
        this.value = (int)Enum.Parse(typeof(CardValue), cardName.ToString());
        this.image = image;
    }

    public enum CardValue
    {
        JACKET = 2, LADY = 3, KING = 4, ACE = 11, SIX = 6,
        SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10
    }

    public enum CardName
    {
        JACKET, LADY, KING, ACE, SIX,
        SEVEN, EIGHT, NINE, TEN
    }
}
