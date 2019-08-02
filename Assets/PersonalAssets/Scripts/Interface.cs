using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class Interface : MonoBehaviour {
	
	public Texture pauseScreen;
	
	public Texture barTexture, reachBarTexture, centerBarTexture, rhythmBarTexture;
	
	public float movingSpeed;//, timePlaying;
	private float initialMovingSpeed;
	public Rect bar, reachBar, centerBar, rhythmBar;
	private Rect initialReachBar, initialCenterBar, initialRhythmBar;
	
	public GameObject player, heart;
	
	private GameObject beepTrail;
	
	private bool goingRight, insideCenter, godMode;
	public bool pressedIt;
	private Bloom[] bloom;
	
	public GameObject soundManager;
	

	void Start () {
//		Debug.Log(Screen.width+" "+ Screen.height);
		beepTrail = GameObject.Find("BeepTrail");
		if (Screen.width > 625 && Screen.height > 469) {
			bar = new Rect((bar.x*Screen.width)/625, (bar.y*Screen.height)/469, (bar.width*Screen.width)/625, (bar.height*Screen.height)/469);
			reachBar = new Rect((reachBar.x*Screen.width)/625, (reachBar.y*Screen.height)/469, (reachBar.width*Screen.width)/625, (reachBar.height*Screen.height)/469);
			centerBar = new Rect((centerBar.x *Screen.width)/625, (centerBar.y*Screen.height)/469, (centerBar.width*Screen.width)/625, (centerBar.height*Screen.height)/469);
			rhythmBar = new Rect((rhythmBar.x*Screen.width)/625, (rhythmBar.y*Screen.height)/469, (rhythmBar.width*Screen.width)/625, (rhythmBar.height*Screen.height)/469);
			
			initialReachBar = reachBar;
			initialCenterBar = centerBar;
			initialRhythmBar = rhythmBar;
			initialMovingSpeed = movingSpeed;
			
		}
		
		//timePlaying = 0f;
		
		goingRight = true;
		insideCenter = false;
		pressedIt = false;
		godMode = false;
	}
	
	void Update () {
		
		MoveRhythmBar();
		CheckRhythmBar();
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale > 0f) {
				Time.timeScale = 0f;
				player.audio.Pause();
			}
			else{
				Time.timeScale = 1f;
				player.audio.Play();
			}
		}
		
		if (heart.renderer.material.color.r <= 0f) {
			player.GetComponent<ItemManager>().OnCharacterHasDied();
		}
		
	}
	
	void OnGUI(){
		//reachBar = new Rect(reachBar.x, reachBar.y, Screen.width/reachBar.width, Screen.height/reachBar.height);
		//GUI.DrawTexture(bar, barTexture);
		//GUI.Box(reachBar, "ReachBar");
//		GUI.DrawTexture (centerBar, centerBarTexture);
		GUI.DrawTexture(rhythmBar, rhythmBarTexture);
		//GUI.DrawTexture(bar, barTexture);
		
		if (Time.timeScale == 0f) {
			GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), pauseScreen);
		}
		
	}
	
	void GotItRight(){
		pressedIt = true;
		//CompleteBeat();
		iTween.PunchScale(heart, iTween.Hash("x",2.34f, "y",2.25f, "z",0.42f, "time",1f));
//		Debug.Log("Got it right");
	}
	void MissedIt(){
		//SubtractCenterBarArea(10f);
		SubtractReachBarArea(20f);
		SetMovingSpeed(0.2f);
		heart.renderer.material.color = new Color(heart.renderer.material.color.r-0.1f, heart.renderer.material.color.g - 0.1f, heart.renderer.material.color.b - 0.1f, heart.renderer.material.color.a);
		//beepTrail.GetComponent<HeartBeat>().speed += 0.05f;
		Camera.main.GetComponent<PulseVignetting>().pulseMax += 0.5f;
	}
	
	void MoveRhythmBar(){
		if (goingRight) {
			if (rhythmBar.x+rhythmBar.width < reachBar.x+reachBar.width) {
				rhythmBar.x += movingSpeed*Time.timeScale;
			}
			else{
				goingRight = false;
				pressedIt = false;
			}
		}
		else{
			if (rhythmBar.x > reachBar.x) {
				rhythmBar.x -= movingSpeed*Time.timeScale;
			}
			else{
				goingRight = true;
				pressedIt = false;
			}
		}
	}
	
	void CheckRhythmBar(){
		
		if (godMode) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				GotItRight();
			}
		}
		else{
			if (centerBar.Contains(rhythmBar.center)) {
				//if(!insideCenter)
					
				insideCenter = true;
				
			}
			else if (!centerBar.Contains(rhythmBar.center) && insideCenter) {
				insideCenter = false;
				if (!pressedIt) {
					MissedIt();
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (insideCenter) {
					soundManager.SendMessage("playSound","HeartBeat");
					GotItRight();
				}
				else{
					MissedIt();
				}
			}
		}

	}
	
	public void SetMovingSpeed(float movingSpeedToBeSet){
		movingSpeed += movingSpeedToBeSet;
	}
	
	public void SubtractCenterBarArea(float subtraction){
		centerBar.x += subtraction;
		centerBar.width -= subtraction*2;
	}
	
	public void AddCenterBarArea(float adding){
		centerBar.x -= adding;
		centerBar.width += adding;
	}
	
	public void SubtractReachBarArea(float subtraction){
		reachBar.width -= subtraction*2;
		reachBar.x += subtraction;
	}
	
	public void AddReachBarArea(float adding){
		reachBar.x -= adding;
		reachBar.width += adding;
	}
	
	public void ActivateGodMode(float duration){
		godMode = true;
//		Camera.main.GetComponent<Vignetting>().intensity = -5f;
//		Camera.main.GetComponent<GlowEffect>().enabled = true;
//		Camera.main.GetComponent<PulseVignetting>().enabled = false;
		bloom = Camera.main.GetComponents<Bloom>();
		foreach(Bloom bl in bloom)
		{
			bl.enabled = true;
		}
		
		Invoke("DeactivateGodMode", duration);
	}
	
	void DeactivateGodMode(){
		godMode = false;
		Camera.main.GetComponent<Vignetting>().intensity = 0f;
		Camera.main.GetComponent<NoiseAndGrain>().enabled = true;
		//Camera.main.GetComponent<Bloom>().enabled = false;
		Camera.main.GetComponent<Tonemapping>().enabled = false;
		Camera.main.GetComponent<NoiseEffect>().enabled = false;
		Camera.main.GetComponent<Vignetting>().enabled = false;
		Camera.main.GetComponent<Vignetting>().chromaticAberration = 0f;
		foreach(Bloom bl in bloom)
		{
			if(bl.bloomIntensity == 0.58f)
			{
				bl.enabled = false;
			}
		}
	}
	
	public float GetLife(){
		return centerBar.width;
	}
	
	public void ResetBars(){
		reachBar = initialReachBar;
		centerBar = initialCenterBar;
		rhythmBar = initialRhythmBar;
		movingSpeed = initialMovingSpeed;
	}
	
}
