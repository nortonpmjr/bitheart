using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralItemGenerator : MonoBehaviour {
	
	
	public List<GameObject> itens;
	public List<int> itensDropRateRange;
	public List<int> itensDroppedTimes;
	
	
	private ProceduralLevelGenerator levelGenerator;
	
	public GameObject Runner;
	public ItemManager player;
	public Interface myInterface;
	
	private bool PlayerHasDefribilator;
	private int range = 0;
	
	public int BlockItemRate;
	
	// Use this for initialization
	void Start () {
		//SetItensRateRange();
		PlayerHasDefribilator = false;
		Runner = GameObject.Find("Runner");
		player = GameObject.FindWithTag("Player").GetComponent<ItemManager>();
		myInterface = GameObject.FindWithTag("Interface").GetComponent<Interface>();
		
		levelGenerator = this.GetComponent<ProceduralLevelGenerator>();
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetItensRateRange()
	{
		for (int i = 0; i < itens.Count; i++)
		{
			range += itens[i].GetComponent<BaseItem>().dropRate;
			itensDropRateRange.Add(range);
		}
				foreach(GameObject go in itens)
		{
			itensDroppedTimes.Add(0);
		}
//		Debug.Log ("FinishedRange");
	}
	
	
	
	public GameObject RollItemChance()
	{
		int rndm = UnityEngine.Random.Range(0,100);
		if(rndm <= BlockItemRate){
			return RollItem(rndm);
		}
		else
		{
			return null;
		}
		
	}
	
	private GameObject RollItem(int rndm)
	{

		if(rndm > itensDropRateRange[itensDropRateRange.Count-1]){
			return null;
		}
		else
		{	
			if(rndm < itensDropRateRange[0])
			{
				itensDroppedTimes[0]++;
				return itens[0];
			}
			else if(rndm < itensDropRateRange[1])
			{
				itensDroppedTimes[1]++;
				return itens[1];
			}
			else if(rndm < itensDropRateRange[2])
			{
				itensDroppedTimes[2]++;
				return itens[2];
			}
			else if(rndm < itensDropRateRange[3])
			{
				itensDroppedTimes[3]++;
				return itens[3];
			}
			else
			{
				return null;
			}
		}
		
	}
	

}
