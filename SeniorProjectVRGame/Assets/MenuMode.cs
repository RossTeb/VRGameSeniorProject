using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMode : MonoBehaviour {
    private static Mode Current_Mode = 0;
    public enum Mode{
        WallPlacement = 0,
        EnemyPlacement = 1,
        TrapPlacement = 2,
        PropPlacement = 3,
        Disabled = 4
    }
	void Start () {
        Mode Current_Mode = Mode.WallPlacement;
	}
	
    public static void SetMode(int x)
    {
        if(x > 3)
        {
            x = 0;
        }
        else if(x< 0)
        {
            x = 3;
        }
        Current_Mode = (Mode)x;
    }

    public static void DisableMenu()
    {
        Current_Mode = (Mode) 4;
    }

    public static int GetCurrentMode()
    {
        int x;
        x = (int)Current_Mode;
        return x;

    }

}
