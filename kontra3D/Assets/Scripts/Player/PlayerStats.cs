﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public static int DefaultPlayerStat = 10;

    /// <summary>
    /// Triggers when Player dies
    /// </summary>
    public event EventHandler PlayerDie;

    /// <summary>
    /// Is called AP value is changed
    /// </summary>
    public event EventHandler<ApChangeEventArgs> OnApValueChanged;

    public PlayerStats() { }

    /// <summary>
    /// Creates playerstats with the given points
    /// </summary>
    /// <param name="health"></param>
    /// <param name="hunger"></param>
    /// <param name="thirst"></param>
    /// <param name="ap"></param>
    public PlayerStats(int health, int hunger, int thirst, int ap)
    {
        Health = health;
        ActionPoints = ap;
        Hunger = hunger;
        Thirst = thirst;
    }

    public int Health = DefaultPlayerStat;
    public int Hunger = DefaultPlayerStat;
    public int Thirst = DefaultPlayerStat;
    public int ActionPoints = DefaultPlayerStat;

    /// <summary>
    /// Update the player stats
    /// </summary>
    /// <param name="difference"></param>
    /// <returns>If Player is still alive</returns>
    public void UpdatePlayerStats(PlayerStats difference, bool removeHealth = false)
    {
        if(difference.ActionPoints != 0)
            NotifyApValueChange(difference.ActionPoints);

        Health = Health + difference.Health;
        Hunger = Hunger + difference.Hunger;
        Thirst = Thirst + difference.Thirst;
        ActionPoints = ActionPoints + difference.ActionPoints;
        
        if (Hunger <= 0)
        {
            Hunger = 0;
            if(removeHealth) Health -= 1;
        }
        else if (Hunger > 10)
        {
            Hunger = 10;
        }

        if (Thirst <= 0)
        {
            Thirst = 0;
            if (removeHealth) Health -= 1;
        }
        else if (Thirst > 10)
        {
            Thirst = 10;
        }

        if (ActionPoints <= 0)
        {
            ActionPoints = 0;
        }
        else if (ActionPoints > 10)
        {
            ActionPoints = 10;
        }

        if (Health <= 0)
        {
            OnPlayerDie();
        }
        else if (Health > 10)
        {
            Health = 10;
        }
    }

    /// <summary>
    /// Execute event when AP value changes
    /// </summary>
    private void NotifyApValueChange(int apChange)
    {
        if (OnApValueChanged != null)
            OnApValueChanged(this, new ApChangeEventArgs(apChange));
    }

    /// <summary>
    /// Execute event when player dies
    /// </summary>
    private void OnPlayerDie()
    {
        if (PlayerDie != null)
        {
            PlayerDie(this, null);
        }
    }
}

public class ApChangeEventArgs : EventArgs
{
    public ApChangeEventArgs(int apChange)
    {
        ApChange = Math.Abs(apChange); 
    }

    public int ApChange { get; set; }
}
