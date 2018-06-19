using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour {

    public Transform playerStatsCanvas;

    Player player;

    Text[] statTexts;

	// Use this for initialization
	void Start ()
    {
        player = Player.playerInstance;
        player.onPlayerStatChangedCallback += UpdateUI;
        
        statTexts = playerStatsCanvas.GetComponentsInChildren<Text>();

        UpdateUI();
	}

    // Update is called once per frame
    void UpdateUI ()
    {
        foreach (var text in statTexts)
        {
            if (text.name == "HealthText")
                text.text = "Health: " + player.Health;

            if (text.name == "HungerText")
                text.text = "Hunger: " + player.Hunger;

            if (text.name == "ThirstText")
                text.text = "Thirst: " + player.Thirst;

            if (text.name == "FortitudeText")
                text.text = "Fortitude: " + player.Fortitude;
        }
    }
}
