using UnityEngine;
using System.Collections;

public class ActivateGodMode : MonoBehaviour {
	
	public GameObject interfaceManager;
	
	// Use this for initialization
	void Start () {
		interfaceManager = GameObject.Find("InterfaceManager");
		interfaceManager.GetComponent<Interface>().ActivateGodMode(20000f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
