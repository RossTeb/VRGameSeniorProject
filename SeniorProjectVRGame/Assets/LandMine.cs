using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour {
    public float Bulb_Transparency = 0.3f;
    private Light blinker;
    private bool blink=true;
    private float BlinkTime;
    public float BlinkInterval;
	// Use this for initialization
	void Start () {
        blinker = GetComponentInChildren<Light>();
        BlinkTime = Time.time+ BlinkInterval;
    }
	
	// Update is called once per frame
	void Update () {
        if (BlinkTime < Time.time)
        {
            blinker.enabled = !blinker.enabled;
            BlinkTime = BlinkTime + BlinkInterval;
        }

	}
}
