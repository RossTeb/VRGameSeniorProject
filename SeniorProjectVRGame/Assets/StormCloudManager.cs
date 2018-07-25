using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloudManager : MonoBehaviour
{
    public GridBehavior Grid;
    public GameObject LightEnd;
    public float destroyTime;
    bool doOnce = true;
    public GameObject Sun;
    public Material Sunny;
    public Material Cloudy;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetCurrentState()==GameStateManager.Types.Build)
        {
            var lastObj = Grid.GetLastObjectPlaced();
            if (!Grid.GetSolvable() && lastObj)
            {
                try
                {

                    LightEnd.transform.position = lastObj.transform.position;
                    GetComponent<LineRenderer>().enabled = true;
                    if (doOnce)
                    {
                        GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                        destroyTime = Time.time + 1.5f;
                        doOnce = false;
                        GetComponent<AudioSource>().enabled = true;
                        if (!Cloudy.Equals(null))
                        {
                            RenderSettings.skybox = Cloudy;
                            RenderSettings.ambientGroundColor = Color.blue;
                            RenderSettings.ambientLight = Color.blue;
                            RenderSettings.sun.color = Color.blue;
                        }
                    }
                    if (Time.time >= destroyTime)
                    {
                        try
                        {
                            Grid.RemoveLastObject();
                        }
                        catch { }

                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else if (!doOnce)
            {
                GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                GetComponent<AudioSource>().enabled = false;
                GetComponent<LineRenderer>().enabled = false;
                doOnce = true;
                if (!Cloudy.Equals(null))
                {
                    RenderSettings.skybox = Sunny;
                    RenderSettings.ambientGroundColor = Color.white;
                    RenderSettings.ambientLight = Color.white;
                    RenderSettings.sun.color = Color.white;
                }
            }

        } 
    }
}
