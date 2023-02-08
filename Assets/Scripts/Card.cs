using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField]
    private string _imageName;
    public string imageName { get { return _imageName; } private set { _imageName = value; } }
    [SerializeField]
    private byte _value;
    public int value { get{return _value;} private set{_value = ((byte)value);} }

    public Card(int value, Sprite image)
    {
        CardName cardName = (CardName)value;
        this.value = (int)Enum.Parse(typeof(CardValue), cardName.ToString());
        imageName = image.name;
    }
    public Sprite GetCardSprite() {
        return Resources.Load<Sprite>("Pics/Playing Cards/image/"+_imageName);
    }

    #region Serialization
    public static Card Deserialize(string json)
    {
        return JsonUtility.FromJson<Card>(json);

    }
    public static string Serialize(Card card)
    {
        return JsonUtility.ToJson(card);
    }
    #endregion
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
