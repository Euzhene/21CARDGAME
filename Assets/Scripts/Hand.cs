using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.euzhene.twentyone
{
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
        public bool visible { get; private set; }
        public bool stand { get; set; } = false;

        public ScoreChangedCallback scoreChanged = null;

        public Hand(bool visible, Transform transform)
        {
            this.visible = visible;
            this.transform = transform;
        }

        public void TakeCard(Card newCard)
        {
            cards.Add(newCard);
            score += newCard.value;
        }
    }
}