using UnityEngine;
using System.Collections;

public class Seringa : BaseItem {

	// Use this for initialization
	void Start () {
		dropRate = 20;
		myName = "Adrenaline";
		effect = Effect.Health;
	}
	
	void OnTriggerEnter(Collider player)
	{
		if(player.name == "Runner")
			player.SendMessage("ReceiveItem", myName);
		GameObject.Find ("SoundManager").SendMessage("playSound","itemCatch");
	}
	
}
