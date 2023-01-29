using System.Collections;
using System.Collections.Generic;
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

    public GameObject cardPrefab;
    public GameObject backCardPrefab;

    Deck deck;

    private void Start()
    {
        deck = new Deck(picsOfClubSuit, picsOfDiamondSuit, picsOfHeartSuit, picsOfSpadeSuit);

        //тут проверка на правильности генерации карт и т.п
        for (int i = 0; i < 2; i++)
        {
            GameObject card = Instantiate(cardPrefab, Vector2.zero, Quaternion.identity, myHand.transform);
            card.GetComponent<Image>().sprite = deck.GiveTopCard().image;
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject card = Instantiate(backCardPrefab, Vector2.zero, Quaternion.identity, enemyHand.transform);
            deck.GiveTopCard();
        }
    }
}

