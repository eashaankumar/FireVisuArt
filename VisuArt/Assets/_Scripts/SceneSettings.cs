using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour {

	public bool isVR;
	public GameObject fpsController, vrController;

	public static bool IS_VR = false;

	// Update is called once per frame
	void Start () {
		IS_VR = isVR;
		if (isVR) {
			fpsController.GetComponentInChildren<Transform> ().gameObject.SetActive (false);
			fpsController.SetActive (false);

			vrController.GetComponentInChildren<Transform> ().gameObject.SetActive (true);
			vrController.SetActive (true);
		} else {
			fpsController.GetComponentInChildren<Transform> ().gameObject.SetActive (true);
			fpsController.SetActive (true);

			vrController.GetComponentInChildren<Transform> ().gameObject.SetActive (false);
			vrController.SetActive (false);
		}
	}
}
