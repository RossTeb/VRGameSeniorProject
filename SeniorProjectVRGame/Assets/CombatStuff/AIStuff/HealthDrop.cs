using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDrop : MonoBehaviour {

    public GameObject healItem;
    public GameObject damageItem;
    //public Transform player;
    public Slider playerHealth;
    private bool hasDropped;

    // Use this for initialization
    void Start () {
        //player = GetComponent<ChaseAI>().player;
        playerHealth = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();

        hasDropped = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<DetectDamagingHitEnemy>().health <= 0 && !hasDropped)
        {
            if (playerHealth.value < (playerHealth.maxValue) / 2)
            {
                this.dropItem(healItem);
                hasDropped = true;
            }
            else
            {
                if(Random.Range(1, 100) <= 50)
                {
                    this.dropItem(damageItem);
                    hasDropped = true;
                }
            }
        }
	}

    public void dropItem(GameObject itemToDrop)
    {
        Instantiate(itemToDrop, this.transform.position, this.transform.rotation);
    }
}
