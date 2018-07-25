using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class start_vr : MonoBehaviour {
    public GameObject cameraRig;
	// Use this for initialization
	void Start () {
        //SceneManager.UnloadSceneAsync(1);
        //UnityEngine.VR.VRSettings.enabled = true;
       // Debug.Log(UnityEngine.VR.VRSettings.supportedDevices);
        UnityEngine.XR.XRSettings.LoadDeviceByName("OpenVR");
       // Instantiate(cameraRig);
       // Display.displays[1].Activate();
        UnityEngine.XR.XRSettings.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
