using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCwin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameStateManager.SetCurrentState(GameStateManager.Types.PCWin);
        }
       
    }

	// Update is called once per frame
	void Update () {
		
	}
}
