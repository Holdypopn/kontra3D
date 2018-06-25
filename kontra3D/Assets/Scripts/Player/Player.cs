using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera MainCamera;
    #region Singleton
    public static Player playerInstance;

    void Awake()
    {
        if (playerInstance != null)
        {
            Debug.LogWarning("More than one instance of Player found!");
            return;
        }

        playerInstance = this;
    }
    #endregion

    /// <summary>
    /// Is called when status is changed
    /// </summary>
    public delegate void OnPlayerStatChanged();
    public OnPlayerStatChanged onPlayerStatChangedCallback;

    /// <summary>
    /// Stats of the Player
    /// </summary>
    public PlayerStats Playerstats;

    public Player()
    {
        Playerstats = new PlayerStats();
        Playerstats.PlayerDie += Playerstats_PlayerDie;
    }

    private void Playerstats_PlayerDie(object sender, EventArgs e)
    {
        SceneManager.LoadScene("GameoverMenu");
    }

    public void Start()
    {
        Inventory.Instance.ItemUsed += InventoryInstance_ItemUsed;
    }

    /// <summary>
    /// Executed when item is used
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InventoryInstance_ItemUsed(object sender, InventoryEventsArgs e)
    {
        var ps = new PlayerStats();

        ps.Health = (e.Item is InventoryItem_Health) ? (e.Item as InventoryItem_Health).HealthPoints : 0;
        ps.Hunger = (e.Item is InventoryItem_Food) ? (e.Item as InventoryItem_Food).FoodPoints : 0;
        ps.Thirst = (e.Item is InventoryItem_Drink) ? (e.Item as InventoryItem_Drink).DrinkPoints : 0;
        ps.ActionPoints = 0;
        Playerstats.UpdatePlayerStats(ps);
        
        OnPlayerStatsChanged();
    }

    /// <summary>
    /// Player search new items and loose AP
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    internal bool Scavange(int points = 1)
    {
        if (Playerstats.ActionPoints < points)
        {
            return false;
        }

        Playerstats.UpdatePlayerStats(new PlayerStats(0, -1, -1, -points), true);
        OnPlayerStatsChanged();
        return true;
    }

    /// <summary>
    /// Player sleeps and gets AP
    /// </summary>
    public void Sleep()
    {
        if(Playerstats.Hunger ==0 || Playerstats.Thirst == 0)
        {
            Playerstats.UpdatePlayerStats(new PlayerStats(-2, -1, -1, 2));
        }
        Playerstats.UpdatePlayerStats(new PlayerStats(0, -1, -1, 2));
        OnPlayerStatsChanged();
    }


    /// <summary>
    /// Execute event when Player stats changes
    /// </summary>
    private void OnPlayerStatsChanged()
    {
        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    
}
