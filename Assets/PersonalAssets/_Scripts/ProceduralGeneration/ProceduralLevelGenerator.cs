using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("PCG/Level Generator")]

public class ProceduralLevelGenerator : MonoBehaviour {

	
	public List<GameObject> Blocks = new List<GameObject>();
	public List<GameObject> Track = new List<GameObject>();
	public List<GameObject> Trigger = new List<GameObject>();
	public GameObject LevelTrigger;
	public int blocksQuant = 0;
	
	private GameObject lastBlock;
	public bool createBlock = false;
	
	public ProceduralItemGenerator itemGenerator;
	
	//Auxiliary Variables
	private GameObject clone;
	private GameObject ToBeDestroyed;
	private int rndm;
	
	
	// Use this for initialization
	void Start () {
		itemGenerator.SendMessage("SetItensRateRange");
		MountTrack();
		OrderBlocks();
	}
	
	// Update is called once per frame
	void Update () {
		if(createBlock)
		{
			RunTimeBlockingAdd(5);
			createBlock = false;
		}
	}
	
	
	
	
	private void MountTrack()
	{
		for(int i = 0; i < blocksQuant; i++)
		{
			rndm = Random.Range(0,Blocks.Count);
			
			clone = Instantiate(Blocks[rndm], transform.position, Quaternion.identity) as GameObject;
			clone.transform.parent = transform;
			clone.transform.rotation = Quaternion.Euler(0,180,0);
			Track.Add(clone);
			//Item Generator
			if(clone.GetComponent<BlockPivots>().HasItemSlot)
			{
				GameObject temp = itemGenerator.RollItemChance();
				if(temp != null)
				{
					//Get position to create the item
					Transform blockTransform = clone.GetComponent<BlockPivots>().ItemSlot;
					//Create item
					GameObject itemClone = Instantiate(temp, blockTransform.position, blockTransform.rotation) as GameObject;
					itemClone.transform.parent = clone.transform;					
				}
			}
		}
	}
	
	private void OrderBlocks()
	{
		for(int i = 0; i < Track.Count-1; i++)
		{
			Track[i+1].GetComponent<BlockPivots>().begin.position = Track[i].GetComponent<BlockPivots>().end.position;
			if(i > 5 && i%5 == 0)
			{
				GameObject triggerClone;
				triggerClone = Instantiate(LevelTrigger, transform.position, Quaternion.identity) as GameObject;
				triggerClone.transform.parent = transform;
				triggerClone.transform.position = Track[i].GetComponent<BlockPivots>().end.position;
				Trigger.Add(triggerClone);
			}
		}
	}
	
	private void RunTimeBlockingAdd(int amount)
	{
		CreateMultipleBlocks(amount);
		DestroyBlocks(amount);

	}
	
	private void DestroyBlocks(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			//Destroy first object
			ToBeDestroyed = Track[0];
			Track.Remove(Track[0]);
			Destroy(ToBeDestroyed.gameObject);	
		}
	}
	
	private void DestroyTrigger(GameObject trigger)
	{
			//Destroy first object
			ToBeDestroyed = trigger;
			Trigger.Remove(Trigger[0]);
			Destroy(trigger);	
	}
	
	private void CreateMultipleBlocks(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			//Check Last Object
			lastBlock = Track[Track.Count-1];
			rndm = Random.Range(0,Blocks.Count);
			
			//Instatiate and add to qeue
			clone = Instantiate(Blocks[rndm], transform.position, Quaternion.identity) as GameObject;
			clone.transform.parent = transform;
			clone.transform.rotation = Quaternion.Euler(0,180,0);
			clone.GetComponent<BlockPivots>().begin.position = lastBlock.GetComponent<BlockPivots>().end.position;
			Track.Add(clone);
			
			//Item Generator
			if(clone.GetComponent<BlockPivots>().HasItemSlot)
			{
				GameObject temp = itemGenerator.RollItemChance();
				if(temp != null)
				{
					//Get position to create the item
					Transform blockTransform = clone.GetComponent<BlockPivots>().ItemSlot;
					//Create item
					GameObject itemClone = Instantiate(temp, blockTransform.position, blockTransform.rotation) as GameObject;
					itemClone.transform.parent = clone.transform;					
				}
			}
			//End Item Generator
			
			if(i%5 == 0)
			{
				GameObject triggerClone;
				triggerClone = Instantiate(LevelTrigger, transform.position, Quaternion.identity) as GameObject;
				triggerClone.transform.parent = transform;
				triggerClone.transform.position = clone.transform.position;
				Trigger.Add(triggerClone);
				
				//Destroy Trigger
				DestroyTrigger(Trigger[0]);
				
			}
		}
	}
	
	private void Respawn()
	{
		CreateMultipleBlocks(5);
		DestroyBlocks(2);
	}
}
