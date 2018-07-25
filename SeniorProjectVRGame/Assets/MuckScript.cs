using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MuckScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
            player.m_WalkSpeed = player.m_WalkSpeed / 3;
            player.m_RunSpeed = player.m_RunSpeed / 3;
            Debug.Log("Enter plsyer");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
            player.m_WalkSpeed = player.m_WalkSpeed * 3;
            player.m_RunSpeed = player.m_RunSpeed * 3;
            Debug.Log("Exit plsyer");
        }
    }


}
