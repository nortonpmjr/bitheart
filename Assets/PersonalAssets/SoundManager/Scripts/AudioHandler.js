var audioEmitter : GameObject;

var audioClips : AudioClip[];
var soundPrefab : GameObject;


/*function Update () {
	if(Input.GetKeyDown(KeyCode.A)){
		handleSound("pulo");
	}
	if(Input.GetKeyDown(KeyCode.Space)){
		handleSound("furadeira");
	}
}

function handleSound(audioName : String){
	playSound(audioName);
}*/

function playSound(audioName : String){
	
	var playClip : int = 0;
	
	for(var clipValue = 0; clipValue < audioClips.Length; clipValue++ ){
		if(audioClips[clipValue].name ==  audioName){
			playClip = clipValue;
		}
	}
	
	var sound : AudioSource;
	 sound = Instantiate(soundPrefab.GetComponent("AudioSource"), transform.position, Quaternion.identity);
	 sound.clip = audioClips[playClip];
}