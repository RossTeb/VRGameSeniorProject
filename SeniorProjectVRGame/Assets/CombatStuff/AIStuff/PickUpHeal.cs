using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpHeal : MonoBehaviour {
    private GameObject playerHealth;
    public Transform player;
    public float range;
    public Transform playerPos;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = player.GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("HealthBar");
    }

    // Update is called once per frame
    void Update () {
        if (Vector3.Distance(playerPos.position, transform.position) < range)
        {
            player.SendMessage("EditHealthBar", 45);
            Destroy(gameObject);
            Debug.Log("object should be dead");
        }
	}
}
