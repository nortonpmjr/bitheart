using UnityEngine;
using System.Collections;

public class BaseItem : MonoBehaviour {
	
	
	public enum Effect {Health, Speed, Life, All};
	
	public int dropRate;
	public Effect effect;
	public string myName;	
	
	
	public int GetDropRate()
	{
		return dropRate;
	}
	
	
}
