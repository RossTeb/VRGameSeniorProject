using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pc_ready : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void readyPC()
    {
        
        GameStateManager.PCReady = true;
        GetComponent<Image>().color = new Color(0, 255, 0);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
