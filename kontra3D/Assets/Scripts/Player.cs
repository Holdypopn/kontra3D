using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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

    public delegate void OnPlayerStatChanged();
    public OnPlayerStatChanged onPlayerStatChangedCallback;

    public PlayerStats playerstats;

    public Player()
    {
        playerstats = new PlayerStats();
        playerstats.PlayerDie += Playerstats_PlayerDie;
    }

    private void Playerstats_PlayerDie(object sender, EventArgs e)
    {
        SceneManager.LoadScene("GameoverMenu");
    }

    public void Start()
    {
        Inventory.inventoryInstance.ItemUsed += InventoryInstance_ItemUsed;
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
        playerstats.UpdatePlayerStats(ps);

        OnPlayerStatsChanged();
    }

    internal bool Scavange(int points = 1)
    {
        if (playerstats.ActionPoints < points)
        {
            return false;
        }

        playerstats.UpdatePlayerStats(new PlayerStats(0, -1, -1, -points), true);
        OnPlayerStatsChanged();
        return true;
    }


    //TODO are the methods necessary??
    public void Sleep()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, -1, -2, 2));
        OnPlayerStatsChanged();
    }

    public void Eat()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, 3, 0, -2));
        OnPlayerStatsChanged();
    }

    public void Drink()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, 0, 2, -1));
        OnPlayerStatsChanged();
    }

    public void Heal()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(3, 0, 0, -3));
        OnPlayerStatsChanged();
    }

    public void TakeDamage()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(-3, 0, 0, 0));
        OnPlayerStatsChanged();
    }

    public void GetThristy()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, 0, -3, 0));
        OnPlayerStatsChanged();
    }

    public void GetHungry()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, -3, 0, 0));
        OnPlayerStatsChanged();
    }

    public void GetWeak()
    {
        playerstats.UpdatePlayerStats(new PlayerStats(0, 0, 0, -3));
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

    #region TODO: Add item, only for testing suppose
    public void AddItemApplejuice()
    {
        Inventory.inventoryInstance.AddItem("Applejuice");
    }
    public void AddItemKnife()
    {
        Inventory.inventoryInstance.AddItem("Knife");
    }
    public void AddItemStrawberry()
    {
        Inventory.inventoryInstance.AddItem("Strawberry");
    }
    public void AddSteak()
    {
        Inventory.inventoryInstance.AddItem("Steak");
    }
    public void AddAtibiotic()
    {
        Inventory.inventoryInstance.AddItem("Atibiotic");
    }
    #endregion
}
