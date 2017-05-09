using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	private const short SPAWN_CODE = 888;

	[SerializeField]
	Behaviour[] componentsToDisable;
	[SerializeField]
	ArtHolder holder;
	[SerializeField]
	MeshRenderer body;

	public static bool IS_CLIENT = true;

	Camera sceneCam;

	void Start(){
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
			holder.DisableControllable ();
		} else {
			sceneCam = GameObject.FindGameObjectWithTag ("SceneCamera").GetComponent<Camera> ();
			if (sceneCam != null) {
				sceneCam.gameObject.SetActive (false);
			}
			holder.EnableControllable ();
		}
		if (!isLocalPlayer) {
			body.material.color = Random.ColorHSV ();
		}
	}

	void OnEnable()
	{
		NetworkServer.RegisterHandler(SPAWN_CODE, ReceiveSpawnInfo);
	}

	void OnDisable(){
		if (sceneCam != null) {
			sceneCam.gameObject.SetActive (true);
		}
	}

	void Update(){
		IS_CLIENT = isClient;
	}

	void ReceiveSpawnInfo(NetworkMessage msg){
		SpawnMessage message = msg.ReadMessage<SpawnMessage> ();

		GameObject go = Instantiate (Resources.Load (message.name), message.pos, message.rot) as GameObject;

		NetworkServer.Spawn(go);
	}

	/*void SpawnObjectDUMMY(NetworkMessage msg)
	{
		print("SERVER: Spawning object");
		NetworkServer.Spawn(Instantiate(dummy, transform.position, Quaternion.identity));
	}*/

	public void SpawnObject(string name, Vector3 position, Quaternion rotation){

		if (!isLocalPlayer)
			return;
		SpawnMessage message = new SpawnMessage ();
		message.name = name;
		message.pos = position;
		message.rot = rotation;
		NetworkManager.singleton.client.Send(SPAWN_CODE, message);
	}
}
		
public class SpawnMessage : MessageBase{
	public string name;
	public Vector3 pos;
	public Quaternion rot;
}