using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour {

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

    public int Health;
    public int Hunger;
    public int Thirst;
    public int Fortitude;

    public Player()
    {
        Health = 100;
        Hunger = 100;
        Thirst = 100;
        Fortitude = 100;
    }

    public void Sleep()
    {
        Fortitude += 10;
        if (Fortitude > 100)
            Fortitude = 100;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Eat()
    {
        Hunger += 10;
        if (Hunger > 100)
            Hunger = 100;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Drink()
    {
        Thirst += 10;
        if (Thirst > 100)
            Thirst = 100;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Heal()
    {
        Health += 10;
        if (Health > 100)
            Health = 100;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void TakeDamage()
    {
        Health -= 10;
        if (Health < 0)
            Health = 0;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetThristy()
    {
        Thirst -= 10;
        if (Thirst < 0)
            Thirst = 0;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetHungry()
    {
        Hunger -= 10;
        if (Hunger < 0)
            Hunger = 0;

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetWeak()
    {
        Fortitude -= 10;
        if (Fortitude < 0)
            Fortitude = 0;

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
