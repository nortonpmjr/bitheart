using UnityEngine;
using System.Collections;

public class HeartBeatTest : MonoBehaviour {
	
	private GameObject interfaceManager;
	
	void Start(){
		interfaceManager = GameObject.Find("InterfaceManager");
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && interfaceManager.GetComponent<Interface>().pressedIt) {
			iTween.ScaleTo(gameObject, iTween.Hash("x",2.34f, "y",2.25f, "z",0.42f, "time",0.25, "oncomplete", "CompleteBeat"));
		}
	}
	
	void CompleteBeat(){
		iTween.ScaleTo(gameObject, iTween.Hash("x",1.56f, "y",1.5f, "z",0.28f, "time",0.25));
	}
	
}
