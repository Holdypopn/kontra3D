using System.Collections;
using System.Collections.Generic;
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
}
