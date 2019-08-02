using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	
	
	public float animationSpeed;
	public GameObject animatedRunner;
	
	private bool pressedUp;
	
	private CharacterMotor motor;
	
	void Start(){
		motor = GetComponent<CharacterMotor>();
		animatedRunner.animation["Run"].speed = animationSpeed;
		
	}
	
	void Update () {
		if (animatedRunner.animation["Run"].speed != animationSpeed) {
			animatedRunner.animation["Run"].speed = animationSpeed;
		}
		
		if (motor.IsGrounded() && !Input.GetKey(KeyCode.DownArrow)) {
			pressedUp = false;
			animatedRunner.animation.CrossFade("Run");
		}
		else if (motor.IsGrounded() && Input.GetKey(KeyCode.DownArrow)) {
			pressedUp = false;
			animatedRunner.animation.CrossFade("Default Take");
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			pressedUp = true;	
		}
		if (Input.GetKey(KeyCode.UpArrow) && pressedUp) {
			animatedRunner.animation.CrossFade("Jump");
		}
		

		
	}
}
