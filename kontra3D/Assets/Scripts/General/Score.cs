using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
    
    public Text ScoreText;
    
    int ScoreCount = 0;

	// Use this for initialization
	void Start ()
    {
        Player.playerInstance.Playerstats.OnApValueChanged += UpdateScore;
	}

    private void UpdateScore(object sender, ApChangeEventArgs e)
    {
        ScoreCount += e.ApChange;
        ScoreText.text = ScoreCount.ToString();
    }
}
