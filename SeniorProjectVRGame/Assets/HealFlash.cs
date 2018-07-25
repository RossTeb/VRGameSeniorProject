using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealFlash : MonoBehaviour
{

    public GameObject hpanel;
    private Image HealPanelImage;
    public bool healTrigger = false;

    // Use this for initialization
    void Start()
    {
        HealPanelImage = hpanel.GetComponent<Image>();
        HealPanelImage.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (healTrigger)
        {
            healTrigger = false;
            HealPanelImage.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
            HealPanelImage.CrossFadeAlpha(0.0f, 1.5f, false);
        }
    }
}