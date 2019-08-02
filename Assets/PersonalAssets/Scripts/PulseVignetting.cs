using UnityEngine;
using System.Collections;

public class PulseVignetting : MonoBehaviour {
	
	public float increaseRate;
	private float frequencyCounter;
	public float pulseMax;
	
	private bool advancing, gameOver;
		
	
	void Start () {
		advancing = true;
	}
	
	void Update () {
		
		if (gameOver) {
			Camera.main.GetComponent<Vignetting>().intensity += 0.25f;
		}
		
//		if (advancing) {
//			Camera.main.GetComponent<Vignetting>().intensity = Mathf.Lerp(Camera.main.GetComponent<Vignetting>().intensity, pulseMax, increaseRate);
//			if (Camera.main.GetComponent<Vignetting>().intensity >= pulseMax-1f) {
//				advancing = false;
//			}
//		}
//		else{
//			Camera.main.GetComponent<Vignetting>().intensity = Mathf.Lerp(Camera.main.GetComponent<Vignetting>().intensity, 5f, increaseRate);
//			if (Camera.main.GetComponent<Vignetting>().intensity <= 6f) {
//				advancing = true;
//			}
//		}
		
	}
	
	public void GameOver(){
		gameOver = true;
	}
}
