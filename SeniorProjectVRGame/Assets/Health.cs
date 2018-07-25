using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    int health = 100;
    bool isPlayer = false;
    // Use this for initialization
    public Slider healthBar;
	void Start ()
    {
		if(this.GetComponentInChildren<Slider>())
        {
            Debug.Log("player is found");
            isPlayer = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isPlayer)
        {
            healthBar.value = health;
        }
	}

    public void setHealth(int input)
    {
        health = input;
    }

    public int getHealth()
    {
        return health;
    }

}
