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
    public int ActionPoints;

    public Player()
    {
        Health = 10;
        Hunger = 10;
        Thirst = 10;
        ActionPoints = 10;
    }
    
    private void UpdatePlayerStats(int healthChange, int hungerChange, int thirstChange, int AP)
    {
        Health = Health + healthChange;
        Hunger = Hunger + hungerChange;
        Thirst = Thirst + thirstChange;
        ActionPoints = ActionPoints + AP;
        
        if(Health <= 0)
        {
            //Game Over!
        }
        
        if(Hunger <= 0)
        {
            Hunger = 0;
            Health -= 1;
        }
        else if(Hunger > 10)
        {
            Hunger = 10;
        }
        
        if(Thirst <= 0)
        {
            Thirst = 0;
            Health -= 1;
        }
        else if(Thirst > 10)
        {
            Thirst = 10;
        }
        
        if(ActionPoints <= 0)
        {
            ActionPoints = 0;
            Health -= 1;
        }
        else if(ActionPoints > 10)
        {
            ActionPoints = 10;
        }
    }

    public void Sleep()
    {
        UpdatePlayerStats(0, -1, -2, 2);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Eat()
    {
        UpdatePlayerStats(0, 3, 0, -2);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Drink()
    {
        UpdatePlayerStats(0, 0, 2, -1);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void Heal()
    {
        UpdatePlayerStats(3, 0, 0, -3);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void TakeDamage()
    {
        UpdatePlayerStats(-3, 0, 0, 0);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetThristy()
    {
        UpdatePlayerStats(0, 0, -3, 0);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetHungry()
    {
        UpdatePlayerStats(0, -3, 0, 0);

        if (onPlayerStatChangedCallback != null)
            onPlayerStatChangedCallback.Invoke();
    }

    public void GetWeak()
    {
        UpdatePlayerStats(0, 0, 0, -3);

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
