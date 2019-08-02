using UnityEngine;
using System.Collections;

public class AnimateBackground : MonoBehaviour {
	
	public float step;

	void Start () {
	
	}
	

	void Update () {
		renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x-step*Time.timeScale, renderer.material.mainTextureOffset.y);
	}
}
