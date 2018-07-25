using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLameDamage : MonoBehaviour
{
    private bool Damaging;
    private bool frameSkip;
    private Collider player;
	// Use this for initialization
	void Start ()
	{
	    Damaging = false;
	    frameSkip = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Damaging && frameSkip)
	    {
	        player.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
	        frameSkip = false;
	    }
	    else
	    {
	        frameSkip = true;
	    }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Damaging = true;
            player = collider;
        }
    }

    void OnTriggerExit()
    {
        Damaging = false;
    }
}
