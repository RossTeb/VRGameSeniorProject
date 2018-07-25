using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnClickScript : MonoBehaviour, IPointerClickHandler {
	
	public AudioSource clickSound;
	
	// Use this for initialization
	void Start () {
		clickSound = clickSound.GetComponent<AudioSource>();
	}
	
	public void OnPointerClick(PointerEventData eventData){
		clickSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
