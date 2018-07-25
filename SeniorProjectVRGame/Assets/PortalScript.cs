using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour {
    public Transform ExitLocation;
    public float rotationOffset = 90;
    public Text time;
    bool timerExpiered=false;
    public GameObject PortalEntrance;
    public void OnTriggerEnter(Collider other)
    {
        if (ExitLocation&&timerExpiered&&other.tag == "Player")
        {
            other.gameObject.transform.position = ExitLocation.position;
            other.gameObject.transform.Rotate(0, rotationOffset, 0);
            Destroy(gameObject);
        }

    }
    

    void Update()
    {
        if(GameStateManager.GetCurrentState()==GameStateManager.Types.Play)
        {
            PortalEntrance.SetActive(true);
            timerExpiered = true;
        }
    }
}
