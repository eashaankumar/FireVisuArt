using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (ArtObject))]

public class RevolveCenter : MonoBehaviour {

    public float angle;
    public int direction;

    private ArtObject artObj;

	// Use this for initialization
	void Awake () {
        artObj = GetComponent<ArtObject>();
	}

	void Start(){
		float random = Random.value;
		if (random <= 0.5)
			direction = -1;
		else
			direction = 1;
		angle = Random.Range (0f, 60);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (artObj == null)
            return;
        if(artObj.GetArtMode() == ArtObjectMode.NORMAL_MODE)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, angle * direction * Time.deltaTime);
        }
    }
}
