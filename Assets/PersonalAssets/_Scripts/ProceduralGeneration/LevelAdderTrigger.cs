using UnityEngine;
using System.Collections;

[AddComponentMenu("PCG/Endless Trigger")]

public class LevelAdderTrigger : MonoBehaviour {
	
	private GameObject Track;
	
	void Awake()
	{
		Track = GameObject.Find("Track");
	}
	
	void OnTriggerEnter(Collider player)
	{
		if(player.name == "Runner")
		{
			Track.SendMessage("RunTimeBlockingAdd", 5);
		}
	}
}
