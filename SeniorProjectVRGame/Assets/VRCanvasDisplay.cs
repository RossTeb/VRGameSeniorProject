using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRCanvasDisplay : MonoBehaviour
{
    public GridBehavior Grid;
    public Text display;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Grid.GetSolvable())
        {
            display.text = "MAZE UNSOLVABLE";
        }
        else
        {
            display.text = "";
        }
    }
}
