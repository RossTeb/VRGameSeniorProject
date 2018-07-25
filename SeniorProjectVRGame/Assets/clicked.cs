using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void buttonclicked ()
    {
        GameStateManager.VRReady = true;
        GetComponent<Image>().color = new Color(0, 255, 0);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
