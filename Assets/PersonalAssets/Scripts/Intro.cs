using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	public float deltaColorBlack, deltaColorWhite;
	public GameObject video;
	private bool goingBlack;
	private float timer;
	
	void Start () {
		goingBlack = false;
		video.active = false;
	}
	
	void Update () {
		
	if (goingBlack) {
			timer += Time.deltaTime;
		Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, deltaColorBlack);
		if (timer > 2.4f) {
			video.active = true;
		}
	}
	else{
		Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.white, deltaColorWhite);
		if (Camera.main.backgroundColor == Color.white) {
			goingBlack = true;
		}
	}
		
	
	}
}
