using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RockThrow : MonoBehaviour
{
    public Collider target;
    public Camera fpsCamera;
    public float projectileSpeed;

    private float EndTime;
    // Use this for initialization
    void Start () {
        var player = GameObject.FindGameObjectWithTag("Player");
        fpsCamera = player.GetComponentInChildren<Camera>();
        var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
	    var hitInfo = new RaycastHit();
	    if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
	    {
	        target = hitInfo.collider;
	        gameObject.transform.LookAt(hitInfo.point);
	        GetComponent<Rigidbody>().AddForce(transform.forward.x, (transform.forward.y+1), transform.forward.z, ForceMode.Impulse);

        }
        EndTime = Time.time + 5.0f;

    }
	
	// Update is called once per frame
	void Update () {
	    if (EndTime < Time.time)
	    {

	        Destroy(gameObject);
	    }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            collision.collider.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
        }

    }
}
