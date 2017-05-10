using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetManagerCuston : NetworkManager {

	public void JoinGameButton(){
		string hostAddress = Network.player.ipAddress + "";
		base.networkAddress = hostAddress;
		base.StartClient ();
		print("Start Client");
	}
}
