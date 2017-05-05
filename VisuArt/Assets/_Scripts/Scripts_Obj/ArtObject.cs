using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtObject : MonoBehaviour {

    private MeshRenderer renderer;
    private Rigidbody rigid;
    public ArtObjectMode artObjMode;

	private float targetScale;
	private float percent;
	public float spawnModeScale;
	private float normalModeScale;

	// Use this for initialization
	void Awake () {
		normalModeScale = transform.localScale.x;
        renderer = GetComponent<MeshRenderer>();
		if (renderer == null) {
			renderer = gameObject.GetComponentInChildren<MeshRenderer> ();
		}
        Hide();
	}
	
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

	void Update(){
		switch (artObjMode) {
		case ArtObjectMode.SPAWN_MODE:
			break;
		case ArtObjectMode.NORMAL_MODE:
			transform.localScale = Vector3.Lerp (Vector3.one * spawnModeScale * 0.5f , Vector3.one * normalModeScale, percent);
			break;
		}
		percent += Time.deltaTime * 3.5f;
	}

    public void ArtSpawnMode()
    {
        artObjMode = ArtObjectMode.SPAWN_MODE;
		percent = 0;
		transform.localScale = Vector3.one * spawnModeScale;

		/*foreach (Material mat in renderer.materials){
			mat.color = new Color (1, 1, 1, 0.5f);
		}*/
    }

    public void ArtNormalMode()
    {
        artObjMode = ArtObjectMode.NORMAL_MODE;
		percent = 0;
		transform.localScale = Vector3.one * spawnModeScale * 0.5f;
        /*foreach (Material mat in renderer.materials){
			mat.color = new Color (1, 1, 1, 0.5f);
		}*/
        //renderer.material.color = ;
    }

    public ArtObjectMode GetArtMode()
    {
        return artObjMode;
    }
}

public enum ArtObjectMode
{
    SPAWN_MODE, NORMAL_MODE
}