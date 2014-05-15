using System.Collections;
using System.Collections.Generic;

public class eventNode{
	
	private int nodeID = 9;
	private string eventText;
	private List<eventTrigger> triggerList = new List<eventTrigger>();
	private List<eventChoice> choiceList = new List<eventChoice>();
	
	public eventNode(string eventText)
	{
	
		this.eventText = eventText;
		
	}
	
	public eventNode()
	{
	
		
		
	}
	
	public void setNodeID(int ID)
	{
		
		this.nodeID = ID;
		
	}
	
	public bool gotTriggers()
	{
	
		if (triggerList.Count != 0)
		{
		
			return true;
			
		}
		
		return false;
		
	}
	
	public List<eventTrigger> getTriggers()
	{
		
		List<eventTrigger> tempList = new List<eventTrigger>(triggerList);
		return tempList;
		
	}
	
	public int getNodeID()
	{
	
		return nodeID;
		
	}
	
	public void setEventText(string eventText)
	{
	
		this.eventText = eventText;
		
	}
	
	public string getEventText()
	{
		
		return eventText;
		
	}
	
	public void addTrigger(eventTrigger trigger)
	{
		
		triggerList.Add(trigger);
		
	}
	
	public void addChoice(eventChoice choice)
	{
	
		choiceList.Add(choice);
		
	}
	
	public List<eventChoice> getEventChoices()
	{
		List<eventChoice> tempList = new List<eventChoice>();
		
		foreach (eventChoice c in choiceList)
		{
		
			tempList.Add (c);
			
		}
		
		return tempList;

	}
	
	public bool checkChoices()
	{
	
		if (choiceList.Count == 0)
		{
			
			return false;
			
		}
		
		return true;
		
	}
	
	public bool checkResource(string resourceType, int resourceAmount)
	{
		
		for (int i = 0; i < triggerList.Count; i++)
		{
		
			if (triggerList[i].getArgument() == resourceType)
			{
			
				if (resourceAmount > triggerList[i].getAmount())
				{
				
					return false;
					
				}
				
			}
			
		}
		
		return true;
				
	}
	
	public string ToString()
	{
	
		string palautettava = "ID: " + nodeID + ". Text: " + eventText;
		return palautettava;
		
	}
	
}
