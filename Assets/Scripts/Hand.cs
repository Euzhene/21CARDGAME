using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hand
{
    public TextMeshPro scoreText;
    int score;
    public void Scoring(Card[] card)
    {
        score = 0;
        for (int i = 0; i < card.Length; i++)
            score += card[i].value;
    }
}
