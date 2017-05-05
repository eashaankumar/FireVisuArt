using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtHolder : MonoBehaviour {

	public ArtObject[] assets;
	public GameObject lineUtils;
	private Transform player;
	private ArtObject[] spawnPointers;
	private int currentArtSpawnIndex = 0;
	private Vector3 spawnAtLoc;
	private bool showThisMenu = false, showDrawMenu = false;

	private Controller leftTouch, rightTouch;
	private float switchThroughArtWait;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if (SceneSettings.IS_VR) {
			leftTouch = GameObject.FindGameObjectWithTag ("LeftTouch").GetComponent<Controller> ();
			rightTouch = GameObject.FindGameObjectWithTag ("RightTouch").GetComponent<Controller> ();
		}

	}

	// Use this for initialization
	void Start () {
		//Instantiate all spawnPointers
		spawnPointers = new ArtObject[assets.Length];
		for(int i = 0; i < assets.Length; i++)
		{
			ArtObject obj = Instantiate(assets[i], transform.position, Quaternion.identity) as ArtObject;
			obj.Hide();
			obj.ArtSpawnMode();
			Destroy(obj.GetComponent<Rigidbody>());
			foreach (Collider c in obj.gameObject.GetComponents<Collider>())
			{
				c.enabled = false;
			}
			obj.transform.SetParent(transform);
			spawnPointers[i] = obj;
		}
	}

	// Update is called once per frame
	void Update () {
		if (SceneSettings.IS_VR) {
			UpdateVR ();
		} else {
			UpdateReg ();
		}

	}

	void FixedUpdate(){
		Vector3 rayStart = transform.position;

		if (SceneSettings.IS_VR) {
			rayStart = rightTouch.transform.position;
		}
		RaycastHit hit;

		if (Physics.Raycast (rayStart, Camera.main.transform.forward, out hit)) {
			spawnAtLoc = hit.point;
		} else {
			spawnAtLoc = transform.position;
		}
	}

	/**
	 * Update only vr components
	 */
	void UpdateVR(){
		if (player == null) {
			Debug.LogError ("No player present in scene");
			return;
		}

		// Player movement
		Vector2 hInput = OVRInput.Get (OVRInput.RawAxis2D.LThumbstick);
		player.transform.position += leftTouch.transform.forward * hInput.y * 3 * Time.deltaTime + leftTouch.transform.right * hInput.x * 3 * Time.deltaTime;
		//Object Picker Menu
		VRObjMenu();
		//Line Menu
		VRLineMenu();

		if (OVRInput.GetDown(OVRInput.RawButton.A)) {
			showThisMenu = !showThisMenu;
			showDrawMenu = false;
		}
		else if (OVRInput.GetDown(OVRInput.RawButton.B)) {
			showDrawMenu = !showDrawMenu;
			showThisMenu = false;
		}
		switchThroughArtWait += Time.deltaTime;
	}

	void VRLineMenu(){
		if (showDrawMenu) {
			lineUtils.SetActive (true);
		} else {
			lineUtils.SetActive (false);
		}
	}

	void VRObjMenu(){
		if (showThisMenu) {
			leftTouch.HideModel ();
			rightTouch.HideModel ();
			spawnPointers [currentArtSpawnIndex].Show ();
			spawnPointers [currentArtSpawnIndex].transform.position = rightTouch.transform.position;
			spawnPointers [currentArtSpawnIndex].transform.rotation = rightTouch.transform.rotation;


			// Place object
			if (OVRInput.GetDown (OVRInput.RawButton.RIndexTrigger)) {
				ArtObject obj = Instantiate (assets [currentArtSpawnIndex], spawnPointers [currentArtSpawnIndex].transform.position, Quaternion.identity) as ArtObject;
				obj.Show ();
				obj.ArtNormalMode ();
				obj.transform.rotation = spawnPointers[currentArtSpawnIndex].transform.rotation;
			}
			// Switch throuh objects
			if (switchThroughArtWait > 0.5f) {
				Vector2 hInput = OVRInput.Get (OVRInput.RawAxis2D.RThumbstick);
				if (hInput.x != 0) {
					switchThroughArtWait = 0;
				}
				if (hInput.x > 0 && currentArtSpawnIndex < spawnPointers.Length - 1) {
					spawnPointers [currentArtSpawnIndex].Hide ();
					currentArtSpawnIndex++;
				} else if (hInput.x < 0 && currentArtSpawnIndex > 0) {
					spawnPointers [currentArtSpawnIndex].Hide ();
					currentArtSpawnIndex--;
				}
			}
		}
		else {
			spawnPointers [currentArtSpawnIndex].Hide ();
			leftTouch.ShowModel ();
			rightTouch.ShowModel ();
		}

	}

	/**
	 * Update only fps components
	 */
	void UpdateReg(){
		if (showThisMenu) {
			if (player == null) {
				Debug.LogError ("No player present in scene");
			}
			spawnPointers [currentArtSpawnIndex].Show ();
			spawnPointers [currentArtSpawnIndex].transform.LookAt (player);
			spawnPointers [currentArtSpawnIndex].transform.position = Vector3.Lerp (spawnPointers [currentArtSpawnIndex].transform.position, spawnAtLoc, 0.5f);
			Vector3 angles = spawnPointers [currentArtSpawnIndex].transform.eulerAngles;
			angles.z = 0;
			angles.x = 0;
			spawnPointers[currentArtSpawnIndex].transform.eulerAngles = angles;

			// Place object
			if (Input.GetMouseButtonDown (0)) {
				ArtObject obj = Instantiate (assets [currentArtSpawnIndex], spawnAtLoc, Quaternion.identity) as ArtObject;
				obj.Show ();
				obj.ArtNormalMode ();
				obj.transform.LookAt (player);
				obj.transform.rotation = spawnPointers[currentArtSpawnIndex].transform.rotation;

			}
			//Switch through art objects
			if (Input.GetKeyDown (KeyCode.RightArrow) && currentArtSpawnIndex < spawnPointers.Length - 1) {
				spawnPointers [currentArtSpawnIndex].Hide ();
				currentArtSpawnIndex++;
			} else if (Input.GetKeyDown (KeyCode.LeftArrow) && currentArtSpawnIndex > 0) {
				spawnPointers [currentArtSpawnIndex].Hide ();
				currentArtSpawnIndex--;
			}
		} else {
			spawnPointers [currentArtSpawnIndex].Hide ();
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			showThisMenu = !showThisMenu;
		}
	}
		
}
