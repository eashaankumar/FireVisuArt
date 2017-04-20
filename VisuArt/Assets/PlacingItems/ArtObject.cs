using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtObject : MonoBehaviour {

    private MeshRenderer renderer;
    private Rigidbody rigid;

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
		/*foreach (Material mat in renderer.materials){
			mat.color = new Color (1, 1, 1, 0.5f);
		}*/
    }

    public void ArtNormalMode()
    {
		/*foreach (Material mat in renderer.materials){
			mat.color = new Color (1, 1, 1, 0.5f);
		}*/
        //renderer.material.color = ;
    }
}
