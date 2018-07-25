using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Display_Vive : MonoBehaviour
{

    // Use this for initialization

    Text counter;

    void Start()
    {

        counter = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetCurrentState() == GameStateManager.Types.Setup)
        {
            counter.text = (game_settings.buildTime/60).ToString() + ":" + ((game_settings.buildTime % 60)).ToString();
        }
        else if(GameStateManager.GetCurrentState() == GameStateManager.Types.Build)
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
        else if (GameStateManager.GetCurrentState() == GameStateManager.Types.Play)
        {
            counter.text = "";
        }
    }
}