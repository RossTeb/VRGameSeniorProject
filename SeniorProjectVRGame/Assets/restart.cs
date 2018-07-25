using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((GameStateManager.GetCurrentState() ==  GameStateManager.Types.ViveWin) || (GameStateManager.GetCurrentState() == GameStateManager.Types.PCWin))
        {
            gameObject.SetActive(true);
        }
	}

    public void reset()
    {
        //SceneManager.UnloadScene(1);
        SceneManager.LoadScene(1);
    }
}
