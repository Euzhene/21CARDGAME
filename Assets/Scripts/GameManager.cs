using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] picsOfClubSuit;
    public Sprite[] picsOfDiamondSuit;
    public Sprite[] picsOfHeartSuit;
    public Sprite[] picsOfSpadeSuit;

    public GameObject myHand;
    public GameObject enemyHand;

    //карты
    List<int> myCards = new List<int>(); 
    List<int> enemyCards = new List<int>();

    public GameObject cardPrefab;
    public GameObject backCardPrefab;

    Deck deck;

    Hand hand = new Hand();

    public TMP_Text scoreText;

    private void Start()
    {
        deck = new Deck(picsOfClubSuit, picsOfDiamondSuit, picsOfHeartSuit, picsOfSpadeSuit);

        //тут проверка на правильности генерации карт и т.п
        for (int i = 0; i < 2; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, Vector2.zero, Quaternion.identity, myHand.transform);
            Card card = deck.GiveTopCard();
            cardObj.GetComponent<Image>().sprite = card.image;
            myCards.Add(card.value);
        }
        hand.Scoring(myCards, scoreText);

        for (int i = 0; i < 2; i++)
        {
            GameObject cardObj = Instantiate(backCardPrefab, Vector2.zero, Quaternion.identity, enemyHand.transform);
            Card card = deck.GiveTopCard();
            enemyCards.Add(card.value);
        }
    }
}

