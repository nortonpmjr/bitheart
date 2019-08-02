var audioEmitter : AudioSource;
var musicClip : AudioClip[];

function Start(){
	audioEmitter = this.gameObject.GetComponent("AudioSource");
	audioEmitter.loop = true;
	playMusic("InGame");
}

function playMusic(name : String ) {
	var playClip : int = 0;
	
	for(var clipValue = 0; clipValue < musicClip.Length; clipValue++ ){
		if(musicClip[clipValue].name ==  name){
			playClip = clipValue;
		}
	}
	
	var sound : AudioSource;
	//sound = Instantiate(soundPrefab.GetComponent("AudioSource"), transform.position, Quaternion.identity);
	audioEmitter.clip = musicClip[playClip];
	audioEmitter.Play();
}