using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryWater : MonoBehaviour
{
    public GridBehavior Grid;

    private bool doOnce = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!Grid.GetSolvable())
	    {
	        if (doOnce)
	        {
	            doOnce = false;
	            var Meshs = GetComponentsInChildren<MeshRenderer>();
	            foreach (MeshRenderer Mesh in Meshs)
	            {
	                Mesh.material.color=Color.black;
	            }
	        }
	    }
        else if (!doOnce)
	    {
	        var Meshs = GetComponentsInChildren<MeshRenderer>();
	        foreach (MeshRenderer Mesh in Meshs)
	        {
	            Mesh.material.color = Color.cyan;
	        }
            doOnce = true;
        }
	}
}
