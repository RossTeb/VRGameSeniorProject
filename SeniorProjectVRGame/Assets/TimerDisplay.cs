using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

    // Use this for initialization
    
    Text counter; 
    
	void Start () {
        
        counter = GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {
	    if (GameStateManager.GetCurrentState() == GameStateManager.Types.Setup)
	    {
	        if (GameStateManager.PCReady != true)
	        {
	            counter.text = "Not Ready";
	        }
	        else
	        {
	            counter.text = "Ready";
                counter.color = new Color(0,255,0);
            }
	    }
        else if (GameStateManager.GetCurrentState() == GameStateManager.Types.Play)
        {
            counter.text = "";
        }
        else
        {
            if ((int)(GameStateManager.EndTime - GameStateManager.OnStart) / 60 == 0)
            {
                counter.text = ((int)(GameStateManager.EndTime - GameStateManager.OnStart) % 60).ToString();
            }
            else if ((((int)(GameStateManager.EndTime - GameStateManager.OnStart) % 60) < 10) && (int)(GameStateManager.EndTime - GameStateManager.OnStart) / 60 > 0)
            {
                counter.text = ((int)(GameStateManager.EndTime - GameStateManager.OnStart) / 60).ToString() + ":0" + ((int)(GameStateManager.EndTime - GameStateManager.OnStart) % 60).ToString();
            }
            else
            {
                counter.text = ((int)(GameStateManager.EndTime - GameStateManager.OnStart) / 60).ToString() + ":" + ((int)(GameStateManager.EndTime - GameStateManager.OnStart) % 60).ToString();
            }
        }
	}
}
