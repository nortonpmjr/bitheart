using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if (hit.gameObject.tag == "Player") {
			hit.gameObject.GetComponent<ItemManager>().ReceiveItem(name);
			Destroy(gameObject);
		}
	}

}
