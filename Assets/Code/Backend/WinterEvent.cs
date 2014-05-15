using System.Collections;
using System.Collections.Generic;

public class WinterEvent {
	
	private string eventID;
	private string eventType;
	private string eventName;
	private string eventTag;
	private List<eventNode> eventNodes = new List<eventNode>();
	
	public WinterEvent()
	{
		
		
	}
	
	public WinterEvent(string eventID, string eventName)
	{
	
		this.eventID = eventID;
		this.eventName = eventName;
		
	}
	
	public void setTagType(string eventTag)
	{
	
		this.eventTag = eventTag;
		
	}
	
	public string getEventTag()
	{
	
		return eventTag;
		
	}
	
	public void setEventType(string eventType)
	{
	
		this.eventType = eventType;
	
	}
	
	public string getEventType()
	{
	
		return eventType;

	}
	
	public void setEventID(string eventID)
	{
	
		this.eventID = eventID;
		
	}
	
	public string getEventID()
	{
	
		return eventID;
		
	}
	
	public string getEventName()
	{
	
		return eventName;
		
	}
	
	public void setEventName(string eventName)
	{
	
		this.eventName = eventName;
		
	}
	
	public void addEventNode(eventNode node)
	{
		
		eventNodes.Add(node);
	
	}
			
	public List<eventNode> getEventNodes()
	{
			
		return eventNodes;
		
	}
	
	public eventNode getEventNode(int nodeID)
	{
	
		foreach (eventNode e in eventNodes)
		{
		
			if (e.getNodeID() == nodeID)
			{
				
				
				return e;
				
			}
			
						
		}
		
		return eventNodes[0];
		
		
	}
	
	public string ToString()
	{
	
		string palautettava = "";
		
		palautettava += "Event ID: " + eventID + " ";
		palautettava += "Event Name: " + eventName + " ";
		palautettava += "Nodeja: " + eventNodes.Count + ".";
		palautettava += "Node ID:t: ";
		
		foreach (eventNode n in eventNodes)
		{
		
			palautettava += n.getNodeID () + ". ";
			
		}
		
		return palautettava;
		
	}
}
