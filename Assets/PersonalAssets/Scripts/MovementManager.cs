using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
public class MovementManager : MonoBehaviour {
	
	#region Variables	
	
	public GameObject animationObject;
	
	public float speed;
	private float actualSpeed;
	
	private GameObject cameraGuide, backgroundCameraGuide, backgroundCamera;
	
	private CharacterController controller;
	
	private Vector3 directionVector;
	private float directionLength;
	private CharacterMotor motor;
	
	private bool getUp, getDown;
	
	private float PositionConstraint;
	
	#endregion
	
	void Awake () {
		motor = GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController>();
		cameraGuide = GameObject.Find("CameraGuide");
		cameraGuide.transform.position = Camera.main.transform.position;
		backgroundCamera = GameObject.Find("Background Camera");
		actualSpeed = 0f;
		getUp = false;
		getDown = true;
		PositionConstraint = this.transform.position.z;
	}
	
	void Update () {
		
		#region GeneralDebugs
		//Debug.Log (GetComponent<CharacterController>().velocity);
		#endregion
		
		if (actualSpeed != speed) {
			motor.movement.maxSidewaysSpeed = speed;
			actualSpeed = speed;
		}
		
		if (Input.GetKey(KeyCode.DownArrow) && motor.IsGrounded()) {
			if (getDown) {
				controller.height = 0.6f;
				animationObject.transform.position = new Vector3(animationObject.transform.position.x, animationObject.transform.position.y + 0.5f, animationObject.transform.position.z);
				getDown = false;
			}
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) && controller.height < 2.0f) {
			getUp = true;
		}
		
		if (getUp) {
			if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 1f)) {
				controller.height = 2.0f;
				animationObject.transform.position = new Vector3(animationObject.transform.position.x, animationObject.transform.position.y - 0.5f, animationObject.transform.position.z);
				getUp = false;
				getDown = true;
			}
		}
		
		// Get the input vector from kayboard or analog stick
		directionVector = new Vector3(1f, 0, 0);
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = transform.rotation * directionVector;
		Camera.main.transform.position = new Vector3(cameraGuide.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		
		//if (Input.GetKeyDown(KeyCode.UpArrow) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 1f)) {
			motor.inputJump = Input.GetButton("Jump");
		//}
		Constraint();
		
	}
	
	public void SetSpeed(float speedToBeSet){
		speed = speedToBeSet;
	}
	
	
	private void Constraint()
	{
		Vector3 constraint = this.transform.position;
		constraint.z = PositionConstraint;
		this.transform.position = constraint;
	}
	
}
