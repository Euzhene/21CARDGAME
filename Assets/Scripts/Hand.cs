using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    int score;
    public void Scoring(List<int> cardsValue, TMP_Text scoreText)
    {
        score = 0;
        for (int i = 0; i < cardsValue.Count; i++)
            score += cardsValue[i];
        scoreText.text = score.ToString();
    }
}
