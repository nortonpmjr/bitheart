using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {

	public Transform[] targets;
	public float speed;
	
	public float waitUntilReturn;
	private float returnCounter;
	
	private int index;
	
	private bool gameOver;
	
	
	void Start(){
		index = 0;
		gameOver = false;
	}
	
	void Update(){
		if (!gameOver) {
			if (transform.position == targets[0].position) {
			GetComponent<TrailRenderer>().enabled = true;	
			}
			
			transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed);
			if (transform.position == targets[index].position) {
				if (index < targets.Length-2) {
					index++;
				}
				else{
					GetComponent<TrailRenderer>().enabled = false;
					returnCounter += Time.deltaTime;
					if (returnCounter >= waitUntilReturn) {
						transform.position = targets[0].position;
						index = 1;
						returnCounter =0f;
					}
				}
			}
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, targets[6].position, speed/2);
			if (transform.position == targets[6].position) {
				transform.position = targets[0].position;
			}
		}
	}
	
	public void GameOver(){
		gameOver = true;
	}

}
