using UnityEngine;
using UnityEngine.AI;

public class PropItemSettings : MonoBehaviour
{
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