using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour {

	public void ToggleObject(GameObject go)
    {
        go.SetActive(!go.activeInHierarchy);
    }
}
