using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;

namespace com.euzhene.twentyone
{
    public delegate void ScoreChangedCallback();

    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Serializable fields
        public Sprite[] picsOfClubSuit;
        public Sprite[] picsOfDiamondSuit;
        public Sprite[] picsOfHeartSuit;
        public Sprite[] picsOfSpadeSuit;

        public Transform myHandTransform;
        public Transform enemyHandTransform;

        public GameObject cardPrefab;
        public GameObject backCardPrefab;

        public TMP_Text myScoreText;

        #endregion

        private Deck deck;
        private int playerCount = 2;
        private List<Hand> players = new List<Hand>();

        private Hand currentHand;
        #region Constants
        private const int MAX_SCORE_IN_HAND = 21;
        private const int START_AMOUNT_OF_CARDS = 2;
        #endregion
        public override void OnJoinedRoom()
        {
            players.Add(new Hand(visible:false, enemyHandTransform));
        }
        private void Start()
        {
            InitPlayer();
            deck = new Deck(picsOfClubSuit, picsOfDiamondSuit, picsOfHeartSuit, picsOfSpadeSuit);
            ObserveChanges();
            HandOutStartPack();
        }
        private void InitPlayer()
        {

            players.Add(new Hand(visible: true, myHandTransform));
           // players.Add(new Hand(visible: false, enemyHandTransform));
            currentHand = players.First();
        }

        public void Hit()
        {
            GiveOneCard(currentHand);
            ChangeCurrentHand();
        }
        public void Stand()
        {
            currentHand.stand = true;
            ChangeCurrentHand();
        }

        private void ChangeCurrentHand()
        {
            int currentHandIndex = players.IndexOf(currentHand);
            do
            {
                if (currentHandIndex == players.Count - 1)
                {
                    currentHandIndex = 0;
                }
                else
                {
                    currentHandIndex++;
                }
            } while (players[currentHandIndex].stand);
            currentHand = players[currentHandIndex];

        }
        private void HandOutStartPack()
        {
            //пока так
            for (int i = 0; i < START_AMOUNT_OF_CARDS; i++)
            {
                GiveOneCard(players[0]);
            }

            // for (int i = 0; i < START_AMOUNT_OF_CARDS; i++)
            // {
            //     GiveOneCard(players[1]);
            // }
        }
        private void GiveOneCard(Hand hand)
        {
            GameObject prefab = hand.visible ? cardPrefab : backCardPrefab;
            GameObject cardObj = Instantiate(prefab, Vector2.zero, Quaternion.identity, hand.transform);
            Card card = deck.GiveTopCard();
            hand.TakeCard(card);
            if (hand.visible)
                cardObj.GetComponent<Image>().sprite = card.image;
        }

        private void ObserveChanges()
        {
            foreach (Hand hand in players)
            {
                hand.scoreChanged = (() =>
{
    if (!hand.visible)
        Debug.LogFormat("enemy score: {0}", hand.score);
    else
        myScoreText.text = hand.score.ToString();
    CheckPlayerScore(hand);
});
            }

        }
        private void CheckPlayerScore(Hand hand)
        {
            if (hand.score > MAX_SCORE_IN_HAND)
            {
                Debug.Log("Should be game over for you");
            }
        }

    }
}
