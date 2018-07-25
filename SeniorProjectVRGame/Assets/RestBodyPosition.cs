using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestBodyPosition : MonoBehaviour {
    public GameObject ObjectToMove;
    public GameObject ObjectLocation;
    public GameObject RotationObject;
    public float VerticleOffset;
    int damping = 2;

    // Use this for initialization
    void Start()
    {
        ObjectToMove.transform.position = new Vector3(ObjectLocation.transform.position.x, ObjectLocation.transform.position.y- VerticleOffset, ObjectLocation.transform.position.z);
        ObjectToMove.transform.rotation = Quaternion.Euler(ObjectLocation.transform.eulerAngles.x, ObjectToMove.transform.eulerAngles.y, ObjectLocation.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        ObjectToMove.transform.position = new Vector3(ObjectLocation.transform.position.x, ObjectLocation.transform.position.y- VerticleOffset, ObjectLocation.transform.position.z);


        ObjectToMove.transform.LookAt(RotationObject.transform);
        ObjectToMove.transform.rotation = Quaternion.Euler(0, ObjectToMove.transform.eulerAngles.y, 0);
    }
}