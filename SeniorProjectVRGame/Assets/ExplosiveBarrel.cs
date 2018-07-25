using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

    private Transform player;
    public float DangerZone;
    public GameObject explosion;
    public int HP;
    // Use this for initialization
    void Start()
    {
        if (HP == 0)HP = 15;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    public void TakeDamge(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {

            Debug.Log("Exploded");
            if ((this.transform.position - player.transform.position).magnitude < DangerZone) { player.SendMessage("TakeDamage", 30, SendMessageOptions.DontRequireReceiver); }
            explosion.SetActive(true);
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
        
    }
}