using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderScript : MonoBehaviour {

    public Transform controller;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!hit.Equals(null))
        {
            hit = GetComponent<GridBehavior>().hitInfo;
            LineRenderer line = GetComponent<LineRenderer>();
            line.SetPosition(0,controller.transform.position);
            line.SetPosition(1, hit.point);
        }
	}
}
