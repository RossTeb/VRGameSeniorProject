using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_control : MonoBehaviour {
    public RaycastHit hitInfo;
    private Ray ray;
    private Vector3 controller_position;
    SteamVR_Controller.Device device;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (GameStateManager.GetCurrentState()==GameStateManager.Types.Setup)
        {
            device = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
                hitInfo = new RaycastHit();
                var layer = 1 << 12;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layer))
                {
                    hitInfo.collider.gameObject.SendMessage("buttonclicked",SendMessageOptions.DontRequireReceiver);
                }
            } 
        }
        
      
    }
    
}
