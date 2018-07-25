using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuItemSettings : MonoBehaviour
{
    public int x, y;
    public GameObject prefab;
    public int type;
    public Vector3 menudefpos;
    public float ItemSelectedScale;
    public Vector3 initial_scale;
    public int max;
    public int placed;
    void Start()
    {
        menudefpos = transform.localPosition;
    }

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
        var meshs = this.GetComponentsInChildren<NavMeshObstacle>();
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
}