using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitXZtansform : MonoBehaviour {
    public GameObject Start;
    public GameObject Target;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(Start.transform.position.x+(Target.transform.position.x - Start.transform.position.x), this.transform.position.y, Start.transform.position.z+(Target.transform.position.z - Start.transform.position.z));
	}
}
