using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventChoice{
	
	private string description;
	private int destination;
	private int positiveDest;
	private List<eventTrigger> triggerList = new List<eventTrigger>();
	private bool chanceChoice = false;
	private float positiveChance;
		
	public eventChoice(string description, int destination)
	{
	
		this.description = description;
		this.destination = destination;
		
	}
	
	public eventChoice()
	{
	
		
	}
		
	public void setPositiveChance(float positiveChance)
	{
		
		chanceChoice = true;
		this.positiveChance = positiveChance;
		
	}
	
	public void setPositiveDest(int positiveDest)
	{
	
		this.positiveDest = positiveDest;
		
	}
		
	public void setDescription(string description)
	{
	
		this.description = description;
		
	}
	
	public void addTrigger(eventTrigger trigger)
	{
	
		if (triggerList.Contains(trigger) == false)
		{
			
			triggerList.Add (trigger);	
			
		}
		
	}
	
	public List<eventTrigger> getTriggerList()
	{
		
		List<eventTrigger> returnableList = new List<eventTrigger>(triggerList);
		return returnableList;
		
	}
	
	public void setDestination(int destination)
	{
	
		this.destination = destination;
		
	}
	
	public string getDescription()
	{
	
		return this.description;
		
	}
	
	public int getDestination()
	{
		if (chanceChoice == true)
		{
			
			//Debug.Log ("Random chance says positive");
			
			float randomnumber = Random.Range (0f, 1f);
			
			//Debug.Log ("Random: " + randomnumber + ". Positive chance: " + positiveChance);
			
			if (randomnumber <= positiveChance)
			{
				
				return this.positiveDest;	
				
			}
			
			else
			{
				
				//Debug.Log ("Random chance says negative");
			
				return this.destination;
				
			}
			
		}
		
		//Debug.Log ("Not a chance node.");
		
		return this.destination;
		
	}

}
