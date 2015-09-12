using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreKeeper : MonoBehaviour {

    public Text scoreTxt;
    private float score = 0f;

    public void UpdateScore(float value) {
        score += value;
        scoreTxt.text = "Score: " + score;
    }
}
