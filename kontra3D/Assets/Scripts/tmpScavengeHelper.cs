using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tmpScavengeHelper : MonoBehaviour {

    #region Singleton
    public static tmpScavengeHelper scavengeHelperInstance;

    void Awake()
    {
        if (scavengeHelperInstance != null)
        {
            Debug.LogWarning("More than one instance of ScavengeHelper found!");
            return;
        }

        scavengeHelperInstance = this;
    }
    #endregion

    public Transform textCanvas;
    Player player;

    public Text inputText;
    public Text outputText;

    public Toggle drinkToggle;
    public Toggle foodToggle;
    public Toggle healthToggle;

    // Use this for initialization
    void Start ()
    {
        player = Player.playerInstance;

        var asd = textCanvas.GetComponentsInChildren<Text>();

        foreach (var item in asd)
        {
            if (item.name == "InputText")
            {
                inputText = item;
            }

            if (item.name == "OutputText")
            {
                outputText = item;
            }
        }

        inputText.text = "INPUT";
        outputText.text = "OUTPUT";

        var toggles = textCanvas.GetComponentsInChildren<Toggle>();

        foreach (var item in toggles)
        {
            if (item.name == "DrinkToggle")
            {
                drinkToggle = item;
            }

            if (item.name == "FoodToggle")
            {
                foodToggle = item;
            }

            if (item.name == "HealthToggle")
            {
                healthToggle = item;
            }
        }
    }
	


	// Update is called once per frame
	void Update () {
		
	}
}
