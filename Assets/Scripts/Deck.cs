using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Deck
{
    List<Card> cardsStorage = new List<Card>();
    List<Card> leftCards = new List<Card>();
    public Deck(Sprite[] clubArray, Sprite[] diamondArray, Sprite[] heartArray, Sprite[] spadeArray)
    {
        InitDeck(clubArray, diamondArray, heartArray, spadeArray);
        Shuffle();
    }
    public void ResetDeck()
    {
        leftCards.Clear();
    }
    public void Shuffle()
    {
        int shuffleCount = 5; // сколько раз перемешивать карты
        Card temp;
        for (int i = 0; i < shuffleCount; i++)
        {
            for (int j = 0; j < cardsStorage.Count; j++)
            {
                int nextCard = Random.Range(0, cardsStorage.Count);
                temp = cardsStorage[i];
                cardsStorage[i] = cardsStorage[nextCard];
                cardsStorage[nextCard] = temp;
            }
        }
        leftCards.AddRange(cardsStorage);
    }

    public Card GiveTopCard()
    {
        Card topCard = leftCards.Last();
        leftCards.Remove(topCard);
        return topCard;
    }

    private void InitDeck(Sprite[] clubArray, Sprite[] diamondArray, Sprite[] heartArray, Sprite[] spadeArray)
    {
        for (int i = 0; i < clubArray.Length; i++)
        {
            Card card = new Card(i + 2, clubArray[i]);
            cardsStorage.Add(card);
        }
        for (int i = 0; i < diamondArray.Length; i++)
        {
            Card card = new Card(i + 2, diamondArray[i]);
            cardsStorage.Add(card);
        }
        for (int i = 0; i < heartArray.Length; i++)
        {
            Card card = new Card(i + 2, heartArray[i]);
            cardsStorage.Add(card);
        }
        for (int i = 0; i < spadeArray.Length; i++)
        {
            Card card = new Card(i + 2, spadeArray[i]);
            cardsStorage.Add(card);
        }
        
        ResetDeck();
    }

}
