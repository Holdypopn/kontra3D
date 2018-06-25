using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipHandler : MonoBehaviour {
    public GameObject Hovermenu;

	// Use this for initialization
	public void SetActive () {
        if(Hovermenu.transform.GetComponentInChildren<Text>().text.Trim() != "")
            Hovermenu.SetActive(true);
	}

    // Update is called once per frame
    public void SetInactive () {
        Hovermenu.SetActive(false);
    }
}
