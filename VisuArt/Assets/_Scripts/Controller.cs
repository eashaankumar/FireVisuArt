using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public OVRInput.Controller controllerType;

	private MeshRenderer rend;

	// Use this for initialization
	void Awake () {
		rend = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = OVRInput.GetLocalControllerPosition (controllerType);
		transform.localRotation = OVRInput.GetLocalControllerRotation (controllerType);
	}

	public void HideModel(){
		rend.enabled = false;
	}

	public void ShowModel(){
		rend.enabled = true;
	}
}
