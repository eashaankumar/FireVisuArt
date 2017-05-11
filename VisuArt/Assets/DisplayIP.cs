using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayIP : MonoBehaviour {

	public Text ipText;

	// Use this for initialization
	void Start () {
		ipText.text = Network.player.ipAddress;
	}
	
	// Update is called once per frame
	void Update () {
		ipText.text = Network.player.ipAddress;

	}
}
