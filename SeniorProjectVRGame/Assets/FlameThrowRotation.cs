using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowRotation : MonoBehaviour {
    public float rotationSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localEulerAngles = new Vector3(0, Time.time * rotationSpeed, 0);
	}
}
