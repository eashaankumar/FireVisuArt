using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {
	public static ColorManager Instance;
	void Awake(){
		if (Instance == null) {
			Instance = this;
		}
	}
	void OnDestroy(){
		if (Instance == this) {
			Instance = null;
		}
	}

	Color color;

	void OnColorChange(HSBColor color) 
	{
		print (color);
		this.color = color.ToColor();
	}
	public Color GetCurrentColor() {
		return this.color;
	}
}
