using UnityEngine;
using System.Collections;

public class TrailWave : MonoBehaviour {

	public float waveLength;
	public float frequency;
	
	public Transform targetUp;
	public Transform targetDown;
	
	private bool goingUp;
	
	void Start () {
		goingUp = true;
	}
	
	void Update () {
		if (goingUp) {
			transform.position = Vector3.MoveTowards(transform.position, targetUp.position, frequency);
			if (transform.position == targetUp.position) {
				goingUp = false;
			}
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, targetDown.position, frequency);
			if (transform.position == targetDown.position) {
				goingUp = true;
			}
		}
		
	}
}
