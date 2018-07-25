using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour {
    public float projectileSpeed;
    public float duration;
    float EndTime;
	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Play();
        var player=GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.LookAt(player.transform);
        if (projectileSpeed == 0)
        {
            projectileSpeed = 5;
        }
        if (duration == 0)
        {
            duration = 5.0f;
        }
        EndTime = Time.time + duration;
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        if (EndTime < Time.time)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        collider.SendMessage("TakeDamage", 20);
    }
    
}
