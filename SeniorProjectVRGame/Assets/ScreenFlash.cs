using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{
    public GameObject Flash;

    private float DisableTime;
	// Use this for initialization
	void Start ()
    {
		Flash.SetActive(false);
	    DisableTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Time.time >= DisableTime && DisableTime > 0)
	    {
	        Flash.SetActive(false);
        }
	}

    void ApplyDamage()
    {
        Flash.SetActive(true);
        DisableTime = Time.time + 3.0f;
    }
}
