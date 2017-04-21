using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtObject : MonoBehaviour {

    private MeshRenderer renderer;
    private Rigidbody rigid;
    public ArtObjectMode artObjMode;

	// Use this for initialization
	void Awake () {
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

    public void ArtSpawnMode()
    {
        artObjMode = ArtObjectMode.SPAWN_MODE;
		/*foreach (Material mat in renderer.materials){
			mat.color = new Color (1, 1, 1, 0.5f);
		}*/
    }

    public void ArtNormalMode()
    {
        artObjMode = ArtObjectMode.NORMAL_MODE;
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