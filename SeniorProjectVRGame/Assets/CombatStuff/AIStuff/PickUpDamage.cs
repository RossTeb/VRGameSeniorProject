using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDamage : MonoBehaviour {
    private Transform player;
    public float range;
    private float timeOfPickup;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        if ((this.transform.position - player.transform.position).magnitude < range)
        {
            player.SendMessage("alterDamage", 15, SendMessageOptions.DontRequireReceiver);
            timeOfPickup = Time.time;
        }
        if(Time.time >= timeOfPickup + 10)
        {
            player.SendMessage("alterDamage", -15, SendMessageOptions.DontRequireReceiver);
            Destroy(this);
        }
    }
}
