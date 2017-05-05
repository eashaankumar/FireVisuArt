using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Light))]
public class FlickeringLights : MonoBehaviour {

	private Light light;
	private float meanLightIntensity;
	public float speed;
	// Use this for initialization
	void Awake () {
		light = GetComponent<Light> ();
	}

	void Start(){
		meanLightIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = Mathf.Lerp(light.intensity, Mathf.Sin (Time.deltaTime * speed) * 2 + meanLightIntensity, 0.5f);
	}
}
