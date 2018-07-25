using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameStateManager.GetCurrentState() == GameStateManager.Types.PCWin)
        {
            var TextBox = GetComponent<Text>();
            TextBox.text = "You Win";
        }
        else if (GameStateManager.GetCurrentState() == GameStateManager.Types.ViveWin)
        {
            var TextBox = GetComponent<Text>();
            TextBox.text = "You Lose";
        }
	}
}
