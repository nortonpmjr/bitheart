#pragma strict

var Opening : MovieTexture;
var Timing : float = 0;


function Start () {
	this.renderer.material.mainTexture = Opening;
	Opening.Play();
}

function Update()
{
	Timing += Time.deltaTime;
	if(Timing >= 11f)
		Application.LoadLevel("Menu");
}
