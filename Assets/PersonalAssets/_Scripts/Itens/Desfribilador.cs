using UnityEngine;
using System.Collections;


public class Desfribilador : BaseItem {
	

	// Use this for initialization
	void Start () {
		dropRate = 2;
		myName = "Defribilator";
		effect = Effect.Life;
	}
	
	
	void OnTriggerEnter(Collider player)
	{
		if(player.name == "Runner")
			player.SendMessage("ReceiveItem", myName);
		GameObject.Find ("SoundManager").SendMessage("playSound","itemCatch");
	}
	
	
}
