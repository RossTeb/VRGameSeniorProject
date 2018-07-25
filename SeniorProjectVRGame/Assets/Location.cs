using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Location: MonoBehaviour
{
    public int x, y;
    public string name = "";
    public GameObject prefab;
    public int type;
    public int gridType;
    public bool collision_restricted=false;
    public int NumberAllowed;
     bool canplace=true;

    public void RotateRight()
    {
        transform.Rotate(0, 90, 0);
    }
    public void RotateLeft()
    {
        transform.Rotate(0, -90, 0);
    }
    public void enableNavmeshObst()
    {
        var meshs=this.GetComponentsInChildren<NavMeshObstacle>();
        foreach (var mesh in meshs)
        {
            mesh.enabled = true;
        }
    }
    public void disableNavmeshObst()
    {
        var meshs = this.GetComponentsInChildren<NavMeshObstacle>();
        foreach (var mesh in meshs)
        {
            mesh.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (collision_restricted)
        {
            if (other.tag == "Collide")
            {
                Debug.Log("Cantplace");
                canplace = false;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (collision_restricted)
        {
            if (other.tag == "Collide")
            {
                Debug.Log("Canplace");
                canplace = true;
            }
        }
    }

    public bool GetCanPlace()
    {
        return canplace;
    }
}
