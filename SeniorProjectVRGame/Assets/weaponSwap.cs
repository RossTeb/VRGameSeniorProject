using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwap : MonoBehaviour {

    //public GameObject weapon1;
    public GameObject weapon2;
    //public GameObject weapon3;
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    SwapWeapons();
        //}
        if (GameStateManager.GetCurrentState() != GameStateManager.Types.Setup)
        {
            weapon2.SetActive(true);
        }
        else
            weapon2.SetActive(false);
    }

    //void SwapWeapons()
    //{
    //    if (weapon1.activeInHierarchy)
    //    {
    //        weapon1.SetActive(false);
    //        weapon2.SetActive(true);
    //        weapon3.SetActive(false);
    //    }
    //    else if(weapon2.activeInHierarchy)
    //    {
    //        weapon1.SetActive(false);
    //        weapon2.SetActive(false);
    //        weapon3.SetActive(true);
    //    }
    //    else if (weapon3.activeInHierarchy)
    //    {
    //        weapon1.SetActive(true);
    //        weapon2.SetActive(false);
    //        weapon3.SetActive(false);
    //    }
    //}
}
