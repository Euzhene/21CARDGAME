using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace com.euzhene.twentyone
{
    public delegate void ScoreChangedCallback();
    public class Hand
    {
        public Transform transform { get; private set; }
        public List<Card> cards { get; private set; } = new List<Card>();

        private int _score = 0;
        public int score
        {
            get { return _score; }
            private set
            {
                _score = value;
                scoreChanged?.Invoke();
            }
        }
        public bool stand { get; set; } = false;

        public ScoreChangedCallback scoreChanged = null;

        public Hand(Transform transform)
        {
            this.transform = transform;
        }

        public void TakeCard(Card newCard)
        {
            cards.Add(newCard);
            score += newCard.value;
        }
    }
}