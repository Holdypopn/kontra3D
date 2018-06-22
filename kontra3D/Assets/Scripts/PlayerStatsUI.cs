using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    Player player;

    Healthbar[] bars;

	// Use this for initialization
	void Start ()
    {
        player = Player.playerInstance;
        player.onPlayerStatChangedCallback += UpdateUI;

        bars = transform.GetComponentsInChildren<Healthbar>();

        UpdateUI();
	}

    // Update is called once per frame
    void UpdateUI ()
    {
        foreach (var bar in bars)
        {
            if (bar.name == "StatusBarHealth")
                bar.SetHealth(player.playerstats.Health);
            if (bar.name == "StatusBarFood")
                bar.SetHealth(player.playerstats.Hunger);
            if (bar.name == "StatusBarDrink")
                bar.SetHealth(player.playerstats.Thirst);
            if (bar.name == "StatusBarAP")
                bar.SetHealth(player.playerstats.ActionPoints);
        }
    }
}
