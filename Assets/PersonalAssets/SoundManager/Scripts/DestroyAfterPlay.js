
function Start(){
	playAndDie();
	this.audio.Play();
}

var timer : float = 0.0f;

function playAndDie(){
	
	//if(this.name == "HeartBeat"){
		timer += Time.deltaTime;
		if(timer > 0.9f)
			Destroy(this.gameObject);
	//}
	//else
	//{
	//	yield WaitForSeconds(this.GetComponent("AudioSource").clip.length);
	//	Destroy(this.gameObject);
	//}
	
	
}