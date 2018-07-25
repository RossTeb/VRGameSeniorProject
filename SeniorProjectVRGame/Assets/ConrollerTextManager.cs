using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConrollerTextManager : MonoBehaviour {

    private MenuMode mode;
    private GameObject selected;
    string currentselected;
    string previousselected;
    int currentMode;
    int previousMode;
    public Text TopText;
    public Text BottomText;
    public Text SelectedText;
    public Text PlacedText;
   
    enum Types
    {
        Walls = 0,
        Enemies = 1,
        Traps = 2,
        Props = 3,
    }
	// Use this for initialization
	void Start () {
        mode = GetComponent<MenuMode>();
        currentMode = MenuMode.GetCurrentMode();
        TopText.text = GetTopText(currentMode);
        BottomText.text = GetBottomText(currentMode);
        selected = radialControllerMenu.getSelected();
        currentselected = selected.GetComponent<Location>().name;
        previousMode = currentMode;
        previousselected = currentselected;
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameStateManager.GetCurrentState() == GameStateManager.Types.Build)
	    {
	        currentMode = MenuMode.GetCurrentMode();
	        currentselected = radialControllerMenu.getSelected().GetComponent<Location>().name;
	        if (previousMode != currentMode)
	        {
	            UpdateText();
	        }
	        if (previousselected != currentselected)
	        {
	            UpdateSelected();
	        }
	        if (radialControllerMenu.getSelected().GetComponent<Location>().NumberAllowed <=
	            radialControllerMenu.GetNumberPlacedSelectedItem())
	        {
	            PlacedText.color = new Color(255, 0, 0);
	            PlacedText.text = ("No More Available");
	        }
	        else
	        {
	            PlacedText.color = new Color(2, 134, 146);
	            PlacedText.text = (radialControllerMenu.getSelected().GetComponent<Location>().NumberAllowed -
	                               radialControllerMenu.GetNumberPlacedSelectedItem()) + " Available";
	        }
	        previousselected = currentselected;
	        previousMode = currentMode;
	    }
	    else
	    {
	        StopTextDisplay();

	    }
    }

    private void UpdateText()
    {
        TopText.text = GetTopText(currentMode);
        BottomText.text = GetBottomText(currentMode);
    }
    private string GetTopText(int x)
    {
        switch ((Types)x)
        {
            case Types.Enemies:
                return "Traps";               
            case Types.Walls:
                return "Enemies";               
            case Types.Props:
                return "Walls";
            case Types.Traps:
                return "Props";
        }
        return "";

    }
    private string GetBottomText(int x)
    {
        switch ((Types)x)
        {
            case Types.Enemies:
                return "Walls";
            case Types.Walls:
                return "Props";
            case Types.Props:
                return "Traps";
            case Types.Traps:
                return "Enemies";
        }
        return "";

    }
    private void UpdateSelected()
    {
        SelectedText.text = currentselected;
    }

    private void StopTextDisplay()
    {
        TopText.text="";
        BottomText.text = "";
        SelectedText.text = "";
        PlacedText.text = "";
    }
}
