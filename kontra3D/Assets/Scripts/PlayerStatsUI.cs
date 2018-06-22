using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    Player player;

    Text[] statTexts;

	// Use this for initialization
	void Start ()
    {
        player = Player.playerInstance;
        player.onPlayerStatChangedCallback += UpdateUI;
        
        statTexts = transform.GetComponentsInChildren<Text>();

        UpdateUI();
	}

    // Update is called once per frame
    void UpdateUI ()
    {
        foreach (var text in statTexts)
        {
            if (text.name == "HealthText")
                text.text = "Health: " + player.playerstats.Health;

            if (text.name == "HungerText")
                text.text = "Hunger: " + player.playerstats.Hunger;

            if (text.name == "ThirstText")
                text.text = "Thirst: " + player.playerstats.Thirst;

            if (text.name == "APText")
                text.text = "Action Points: " + player.playerstats.ActionPoints;
        }
    }
}
