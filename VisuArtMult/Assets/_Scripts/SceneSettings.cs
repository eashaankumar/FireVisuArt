using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettings : MonoBehaviour {

	public bool isVR;

	public static bool IS_VR = false;

	public Text artObjCountText;

	private float currentCounter;

	// Update is called once per frame
	void Start () {
		IS_VR = isVR;
		/*if (isVR) {
			fpsController.GetComponentInChildren<Transform> ().gameObject.SetActive (false);
			fpsController.SetActive (false);

			vrController.GetComponentInChildren<Transform> ().gameObject.SetActive (true);
			vrController.SetActive (true);
		} else {
			fpsController.GetComponentInChildren<Transform> ().gameObject.SetActive (true);
			fpsController.SetActive (true);

			vrController.GetComponentInChildren<Transform> ().gameObject.SetActive (false);
			vrController.SetActive (false);
		}*/
	}

	void Update(){
		currentCounter += Time.deltaTime;
		if (currentCounter >= 1.5f) {
			currentCounter = 0;
			artObjCountText.text = "Art Objects: " + FindObjectsOfType<ArtObject> ().Length;
		}
	}
}
