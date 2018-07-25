using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var Arrow = 4;
        var King = 8;
        Physics.IgnoreLayerCollision(Arrow, King, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
