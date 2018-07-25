using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPostion : MonoBehaviour {

    public GameObject ObjectToMove;
    public GameObject ObjectLocation;
    public float VerticleOffset;
	// Use this for initialization
	void Start () {
        ObjectToMove.transform.localPosition = new Vector3(0, -2, 0);
        ObjectToMove.transform.localRotation = new Quaternion(0, 0, 0,0);
    }
	
	// Update is called once per frame
	void Update () {
        ObjectToMove.transform.localPosition = new Vector3(0, -2, 0);
        ObjectToMove.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
