using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Mine : MonoBehaviour {
    public GraphicExplosion explosion;
    private bool started;
    AudioSource sound;
    // Use this for initialization
    void Start () {
        //explosion.SetActive(false);
        AudioSource sound = this.GetComponent<AudioSource>();
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStateManager.GetCurrentState() == GameStateManager.Types.Play)
        {
            if (!started)
            {
                GetComponent<SphereCollider>().radius = 50;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            //sound.Play();
            Debug.Log("player entered mine");
          //  explosion.SetActive(true);
            Health player = other.gameObject.GetComponent<Health>();
            player.setHealth(player.getHealth()-5);
            Destroy(this.gameObject);
        }
    }
}
