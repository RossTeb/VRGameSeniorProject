using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemSettings : MonoBehaviour {

    public int x, y;
    public GameObject prefab;
    public int type;
    public Vector3 menudefpos;
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

    }
    public void disableNavmeshObst()
    {

    }
}
