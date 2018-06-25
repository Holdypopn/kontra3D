using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image ImgHealthBar;

    public Text TxtHealth;

    public int Max;

    void Start()
    {

    }

    public void SetHealth(int health)
    {
        TxtHealth.text = health.ToString();

        float currentPercentage = 0;

        if(PlayerStats.DefaultPlayerStat != 0)
        {
            currentPercentage = (float)health / (float)(Max);
        }

        ImgHealthBar.fillAmount = currentPercentage;
    }
}
