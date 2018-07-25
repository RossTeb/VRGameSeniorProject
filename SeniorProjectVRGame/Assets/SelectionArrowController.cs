using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrowController : MonoBehaviour
{
    private bool notInTower;
    public float rotationspeed;
    float runtime;
    private bool doOnce;
	// Use this for initialization
	void Start ()
	{
	    doOnce = true;
	    notInTower = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameStateManager.GetCurrentState() == GameStateManager.Types.Play && notInTower)
	    {
	            GetComponent<MeshRenderer>().enabled = true;
	            var meshes = GetComponentsInChildren<MeshRenderer>();
	            foreach (var mesh in meshes)
	            {
	                mesh.enabled = true;

	            }

	        Vector3 newLocation = this.transform.position;
	        this.transform.position = new Vector3(newLocation.x,newLocation.y+(Mathf.Sin(Time.deltaTime+runtime)- Mathf.Sin(runtime))*0.35f,newLocation.z);
	        transform.Rotate(Vector3.up * Time.deltaTime*rotationspeed, Space.World);
	        runtime += Time.deltaTime;

        }
        else
	    {
	        GetComponent<MeshRenderer>().enabled = false;
	        var meshes = GetComponentsInChildren<MeshRenderer>();
	        foreach (var mesh in meshes)
	        {
	            mesh.enabled = false;

	        }
	    }
	}

    public void SetInTower()
    {
        notInTower = false;
    }
}
