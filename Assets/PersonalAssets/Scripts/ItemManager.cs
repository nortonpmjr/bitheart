using UnityEngine;
using System.Collections;
using System;

public class ItemManager : MonoBehaviour {
	
	
	
	private System.DateTime StartedTime;
	private System.DateTime DeathTime;
	
	public GUIText survivedFor;
	public GUIText StartTime;
	public GUIText EndTime;
	
	public float timePlaying;
	
	public bool hasDefribilator, revive;
	
	public GameObject interfaceManager, defibIcon;
	
	private bool onDrugs;
	private float drugsCounter, totalTimeOnDrugs;
	
	bool debug = false, died;
	bool ShowedTime;
	
	private int RespDistance = 10;
	
	void Start(){
		timePlaying = 0f;
		
		hasDefribilator = false;
		interfaceManager = GameObject.Find("InterfaceManager");
		StartedTime = DateTime.Now;
		
		survivedFor.enabled = false;
		StartTime.enabled = false;
		EndTime.enabled = false;
		ShowedTime = false;
	}
	
	void Update(){
		timePlaying += Time.deltaTime;
		if(debug){
			if(Input.GetKeyUp(KeyCode.Space))
			{
				Relive();
			}
		}
		if (died) {
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {
				Application.LoadLevel(Application.loadedLevel);
			}	
		}
		
		if (revive) {
			if (Camera.main.GetComponent<Vignetting>().intensity >= 25f) {
				audio.Play ();
				Relive();
				revive = false;
				Camera.main.GetComponent<Vignetting>().enabled = false;
				Camera.main.GetComponent<PulseVignetting>().enabled = false;
				Camera.main.GetComponent<Vignetting>().intensity = 0f;
			}
		}
		
		if(this.transform.position.y < -1.5f)
		{
			OnCharacterHasDied();
		}
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Death"){
			OnCharacterHasDied();
		}
	}
	
	public void OnCharacterHasDied(){
		if (hasDefribilator) {
			revive = true;
			GameObject.Find("InterfaceManager").GetComponent<Interface>().ResetBars();
			GameObject.Find("InterfaceManager").GetComponent<Interface>().heart.renderer.material.color = Color.white;
			
			Camera.main.GetComponent<Vignetting>().enabled = true;
			Camera.main.GetComponent<PulseVignetting>().enabled = true;
			Camera.main.GetComponent<PulseVignetting>().GameOver();
			transform.position = new Vector3(transform.position.x, transform.position.y + 100f, transform.position.z);
			hasDefribilator = false;
			defibIcon.renderer.enabled = false;
		}
		else{
			Die();
		}
	}
	
	public void ReceiveItem(string itemName){
		if (itemName == "Defribilator") {
			hasDefribilator = true;
			defibIcon.renderer.enabled = true;
//				Debug.Log ("DefiberON");
		}
		else if (itemName == "GodMode") {
			interfaceManager.GetComponent<Interface>().ActivateGodMode(7f);
			Camera.main.GetComponent<NoiseAndGrain>().enabled = false;
			
			Camera.main.GetComponent<Tonemapping>().enabled = true;
			Camera.main.GetComponent<NoiseEffect>().enabled = true;
			Camera.main.GetComponent<Vignetting>().enabled = true;
			Camera.main.GetComponent<Vignetting>().chromaticAberration = 6.28f;
		}
		else if (itemName == "Adrenaline") {
			if (interfaceManager.GetComponent<Interface>().heart.renderer.material.color.r < 1f) {
				interfaceManager.GetComponent<Interface>().heart.renderer.material.color += new Color(0.3f, 0.3f, 0.3f, 0f);
			}
		}
		else if(itemName == "Poison"){
			interfaceManager.GetComponent<Interface>().heart.renderer.material.color -= new Color(0.3f, 0.3f, 0.3f, 0f);;
		}
//		else if (itemName == "Drugs") {
//			Camera.main.GetComponent<Fisheye>().enabled = true;
//			onDrugs = true;
//		}
	}
	
	void Relive(){
		GameObject.Find ("Track").SendMessage("Respawn");
		GameObject[] RespObjects = GameObject.FindGameObjectsWithTag("Respawn");
		
		foreach(GameObject go in RespObjects)
		{
			if(go.transform.position.x < this.transform.position.x)
			{
				if(debug) Debug.Log ("Atras");
			}
			else{
				if(debug) Debug.Log (go.transform.position.x.ToString());
				if(Mathf.Abs(this.transform.position.x - go.transform.position.x) < RespDistance)
				{
					if(go.name != "bloco05(Clone)"){
						this.transform.position = go.transform.position;
						RespDistance = 10;
					}
					else
						RespDistance += 10;
				}
			}
		}
	}
	
	void Die(){
		
		GetComponent<AnimationManager>().animatedRunner.animation.Stop();
		GetComponent<AnimationManager>().enabled = false;
		GetComponent<CharacterMotor>().enabled = false;
		GameObject.Find("InterfaceManager").GetComponent<Interface>().enabled = false;
		GameObject.Find ("BeepTrail").GetComponent<HeartBeat>().GameOver();
		Camera.main.GetComponent<Vignetting>().enabled = true;
		Camera.main.GetComponent<PulseVignetting>().enabled = true;
		Camera.main.GetComponent<PulseVignetting>().GameOver();
		died = true;
		ShowLifetime();
	}
	
	
 	private void ShowLifetime()
	{
		if(!ShowedTime){
			DeathTime = DateTime.Now;
			survivedFor.enabled = true;
			StartTime.enabled = true;
			EndTime.enabled = true;
			survivedFor.text = "You fought against a heart attack for " + timePlaying.ToString("f0") + " seconds, but death was stronger. Rest in peace.";
			StartTime.text = "Born on: " + StartedTime.ToString();
			EndTime.text = "Died on: " + DeathTime.ToString();
			ShowedTime = true;
		}
	}
}
