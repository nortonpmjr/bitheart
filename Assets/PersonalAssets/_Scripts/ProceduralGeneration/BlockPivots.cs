using UnityEngine;
using System.Collections;

[AddComponentMenu("PCG/Block Pivot")]

public class BlockPivots : MonoBehaviour {

	public Transform begin;
	public Transform end;
	
	public float lenght;
	
	public bool HasItemSlot = false;
	public Transform ItemSlot = null;
	
	
	
	void Awake()
	{
		begin = this.transform;
		lenght = begin.position.x - end.position.x;
	}
}
