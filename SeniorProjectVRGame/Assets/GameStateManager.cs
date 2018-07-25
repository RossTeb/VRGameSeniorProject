using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {



    static Types Current_State;
    static Types Previous_State;
    public GridBehavior Grid;

    private float StartDelay = 10;
    public static bool VRReady=false;
    public static bool PCReady = false;
    public static float OnStart;
    public static float EndTime;
    public float FadeTimer;
    private bool FinishStart;
    private bool PlayStart;
    public GameObject vive_camera1;
//public GameObject vive_camera2;

    public enum Types
    {
        Setup = 0,
        Build = 1,
        Play = 2,
        PCWin = 3,
        ViveWin = 4
    } 
    
    // Use this for initialization
    void Start () {
        VRReady = false;
        PCReady = false;
        Current_State = Types.Setup;
        StartDelay = game_settings.buildTime;

}


    // Update is called once per frame
    void Update() {
        if (Current_State != Types.Setup)
        {
            OnStart = Time.time;

            if (Time.time >= EndTime && !PlayStart && Grid.GetSolvable() && OnStart > 0)
            {
                SetPlayMode();
                PlayStart = true;
                
            }
            if (Current_State == Types.ViveWin || Current_State == Types.PCWin)
            {
                Debug.Log("sTATE cHANGE");
               
                if (!FinishStart)
                {
                    FadeTimer = Time.time + 5.0f;
                    FinishStart = true;
                    Debug.Log("Started is Setf");
                }

            }
            if (FadeTimer <= Time.time && FinishStart)
            {
                Debug.Log("gAMEovER");


                SceneManager.LoadScene(0);
                
                
            } 
        }
        if (Current_State == Types.Setup)
        {
            Cursor.visible = true;
            if (VRReady && PCReady)
            {
                Current_State=Types.Build;
                OnStart = Time.time;
                EndTime = OnStart + StartDelay;
                FadeTimer = -1.0f;
                FinishStart = false;
                PlayStart = false;
            }
        }

	}
    public static Types GetPreviousState()
    {
        return Previous_State;
    }
    public static Types GetCurrentState()
    {
        return Current_State;
    }

    private void SetBuildMode()
    {
        MenuMode.SetMode(4);
    }
    private void SetPlayMode()
    {
        Current_State = Types.Play;
        MenuMode.DisableMenu();
    }
    public static void SetCurrentState(Types state)
    {
        Current_State = state;
    }
    public void readyPC()
    {
        PCReady = true;
    }


}
