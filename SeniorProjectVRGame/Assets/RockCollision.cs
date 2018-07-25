using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnColliderEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            this.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
        }

    }
}
