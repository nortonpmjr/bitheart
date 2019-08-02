using UnityEngine;
using System.Collections;

public class Veneno : BaseItem {

	// Use this for initialization
	void Start () {
		dropRate = 20;
		myName = "Poison";
		effect = Effect.Health;
	}
	
	void OnTriggerEnter(Collider player)
	{
		if(player.name == "Runner")
			player.SendMessage("ReceiveItem", this.myName);
		GameObject.Find ("SoundManager").SendMessage("playSound","itemCatch");
	}
}
