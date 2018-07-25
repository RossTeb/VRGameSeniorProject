using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffect : MonoBehaviour {

    private Transform player;
    public float DangerZone;
    public GameObject explosion;
    private bool notTriggered;
    private float DestroyTime;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        notTriggered = true;
        DestroyTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if((this.transform.position - player.transform.position).magnitude < DangerZone && notTriggered)
        {
            Debug.Log("Exploded");
            player.SendMessage("TakeDamage", 30, SendMessageOptions.DontRequireReceiver);
            explosion.SetActive(true);
            notTriggered = false;
            var meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
            var colliders = gameObject.GetComponentsInChildren<MeshCollider>();
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
            foreach (MeshCollider collider in colliders)
            {
                collider.enabled = false;
            }
            DestroyTime = Time.time + 1.0f;
        }
		else if(!notTriggered&&Time.time >= DestroyTime && DestroyTime>0)
		{
		    Destroy(gameObject);
		}
	}

    void ApplyDamage()
    {
        explosion.SetActive(true);
        notTriggered = false;
        var meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        var colliders = gameObject.GetComponentsInChildren<MeshCollider>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        foreach (MeshCollider collider in colliders)
        {
            collider.enabled = false;
        }
        DestroyTime = Time.time + 1.0f;
    }
}
