using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public float rotationspeed;
    float runtime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       // Vector3 newLocation = this.transform.position;
       // this.transform.position = new Vector3(newLocation.x,newLocation.y+(Mathf.Sin(Time.deltaTime+runtime)- Mathf.Sin(runtime))*0.35f,newLocation.z);
        //transform.Rotate(Vector3.up * Time.deltaTime*rotationspeed, Space.World);
        //runtime += Time.deltaTime;
	}
}
