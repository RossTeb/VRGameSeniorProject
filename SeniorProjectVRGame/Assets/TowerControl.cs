using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour {


    public RaycastHit hitInfo;
    private Ray ray;
    public Transform pointerOriginTransform = null;
    public SteamVR_TrackedController controller1;
    public GameObject Player;
    public GameObject BuildCamera;
    private SteamVR_Controller.Device device;

    public GameObject Arrow;
    // Use this for initialization
    void Start () {
        BuildCamera = GameObject.FindGameObjectWithTag("VRPlayer");
        //device = SteamVR_Controller.Input((int)controllerLeft.GetComponentInParent<SteamVR_TrackedObject>().index);
    }
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.GetCurrentState() == GameStateManager.Types.Play)
        {
            
            if (controller1.triggerPressed)
            {
                Debug.Log("Trigger Pressed");
                
                ray = new Ray(GetOriginPosition(), GetOriginForward());

                hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    Debug.Log("Raycast hit");
                    if (hitInfo.collider == GetComponent<BoxCollider>())
                    {
                        Debug.Log("Switching Cameras");
                        BuildCamera.SetActive(false);
                        Player.gameObject.SetActive(true);
                        Arrow.SetActive(false);                
                    }
                }
            }
        }
        else
        {
            controller1 = BuildCamera.GetComponentInChildren<SteamVR_LaserPointer>().gameObject.GetComponent<SteamVR_TrackedController>();
            pointerOriginTransform = controller1.transform;
        }
	}
    protected virtual Vector3 GetOriginForward()
    {
        return (pointerOriginTransform ? pointerOriginTransform.forward : transform.forward);
    }
    protected virtual Vector3 GetOriginPosition()
    {
        return (pointerOriginTransform ? pointerOriginTransform.position : transform.position);
    }
}
