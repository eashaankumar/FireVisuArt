using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtHolder : MonoBehaviour {

    public ArtObject[] assets;
	private Transform player;
    private ArtObject[] spawnPointers;
    private int currentArtSpawnIndex = 0;
	private Vector3 spawnAtLoc;
	private bool showThisMenu = false;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
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
		if (showThisMenu) {
			if (player == null) {
				Debug.LogError ("No Player Present in scene ");
			}
			spawnPointers [currentArtSpawnIndex].Show ();
			spawnPointers [currentArtSpawnIndex].transform.LookAt (player);
			spawnPointers [currentArtSpawnIndex].transform.position = Vector3.Lerp (spawnPointers [currentArtSpawnIndex].transform.position, spawnAtLoc, 0.5f);
			Vector3 angles = spawnPointers [currentArtSpawnIndex].transform.eulerAngles;
            angles.z = 0;
            angles.x = 0;
            spawnPointers[currentArtSpawnIndex].transform.eulerAngles = angles;


            if (Input.GetMouseButtonDown (0)) {
				ArtObject obj = Instantiate (assets [currentArtSpawnIndex], spawnAtLoc, Quaternion.identity) as ArtObject;
				obj.Show ();
				obj.ArtNormalMode ();
				obj.transform.LookAt (player);
                obj.transform.rotation = spawnPointers[currentArtSpawnIndex].transform.rotation;

            }
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

	void FixedUpdate(){
		RaycastHit hit;

		if (Physics.Raycast (transform.position, Camera.main.transform.forward, out hit)) {
			spawnAtLoc = hit.point;
		} else {
			spawnAtLoc = transform.position;
		}
	}
}
