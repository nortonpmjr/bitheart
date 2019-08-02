using UnityEngine;
using System.Collections;

public class Cruz : BaseItem {

	// Use this for initialization
	void Start () {
		dropRate = 5;
		myName = "GodMode";
		effect = Effect.All;
	}
	
	void OnTriggerEnter(Collider player)
	{
		if(player.name == "Runner")
			player.SendMessage("ReceiveItem", myName);
		GameObject.Find ("SoundManager").SendMessage("playSound","itemCatch");
	}
}