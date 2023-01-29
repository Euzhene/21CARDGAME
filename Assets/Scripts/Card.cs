using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Sprite image;
    public int value{get;private set;}
    
    public Card(int value, Sprite image) {
        this.value = value;
        this.image = image;
    }
}
