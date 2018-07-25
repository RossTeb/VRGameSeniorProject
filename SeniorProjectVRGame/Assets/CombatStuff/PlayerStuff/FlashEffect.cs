using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour {

    public CanvasGroup canvasGroup; // add canvasgroup to base canvas assign it here, its alpha should be 0 and 
                                    // it should not be interactable or block raycast hits
    private bool flash;

	// Use this for initialization
	void Start () {
        flash = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (flash)
        {
            canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                flash = false;
            }
        }
	}

    public void triggerFlash()
    {
        flash = true;
        canvasGroup.alpha = 1;
    }
}
