using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour {

    public GameObject panel;
    private Image panelImage;
    public bool trigger = false;

	// Use this for initialization
	void Start () {
        panelImage = panel.GetComponent<Image>();
        panelImage.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (trigger)
        {
            trigger = false;
            panelImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
            panelImage.CrossFadeAlpha(0.0f, .1f, false);
        }
    }
}
