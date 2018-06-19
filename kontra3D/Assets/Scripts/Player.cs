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
        Debug.Log(Fortitude);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Eat()
    {
        Debug.Log(Hunger);
    }

    public void Drink()
    {
        Debug.Log(Thirst);
    }

    public void Heal()
    {
        Debug.Log(Health);
    }

    public void TakeDamage()
    {
        Debug.Log(Health);
    }

    public Inventory inventory;

    //TODO: Add item, only for testing suppose
    public void AddItemApplejuice()
    {
        inventory.AddItem(ItemType.Applejuice);
    }
    public void AddItemKnife()
    {
        inventory.AddItem(ItemType.Knife);
    }
    public void AddItemStrawberry()
    {
        inventory.AddItem(ItemType.Strawberry);
    }
    public void AddSteak()
    {
        inventory.AddItem(ItemType.Steak);
    }
    public void AddAtibiotic()
    {
        inventory.AddItem(ItemType.Atibiotic);
    }
}
