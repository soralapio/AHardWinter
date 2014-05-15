using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLParser {
	
	XmlDocument doc = new XmlDocument();
	eventChoice tempChoice;
	eventNode tempNode;
	List<eventChoice> tempChoices = new List<eventChoice>();
	WinterEvent tempEvent;
	
	public XMLParser()
	{
		
	}
	
	public WinterEvent parseXMLToEvent(string fileName)
	{
		
		tempChoices.Clear();
		
		//fileName += ".xml";
			
		doc.LoadXml (fileName);
					
		//XmlNodeList readNodeList = doc.DocumentElement.SelectNodes ("winterevent/eventdata/data");
		XmlNode eventInfo = doc.DocumentElement.SelectSingleNode ("/winterevent/eventdata/data");
		
		tempEvent = new WinterEvent();
		
		tempEvent.setEventID(eventInfo.Attributes["id"].Value);
		tempEvent.setEventName(eventInfo.Attributes["eventname"].Value);
		tempEvent.setEventType (eventInfo.Attributes["type"].Value);
		
		List<eventNode> tempNodes = new List<eventNode>();
		
		XmlNodeList readNodeList = doc.DocumentElement.SelectNodes("/winterevent/event/node");
		
		foreach (XmlNode n in readNodeList)
		{		
			tempNode = new eventNode();
			//Debug.Log("ID on: " + System.Convert.ToInt32(n.Attributes["id"].Value));
									
			tempNode.setEventText(n.SelectSingleNode ("say").InnerText);
			tempNode.setNodeID(System.Convert.ToInt32(n.Attributes["id"].Value));
						
			XmlNodeList choices = n.SelectNodes ("line");
			
			if (choices.Count != 0)
			{
				foreach (XmlNode c in choices)
				{
				
					tempChoice = new eventChoice();
					
					if (c.Attributes["positive"] != null)
					{
					
						tempChoice.setPositiveDest(System.Convert.ToInt32 (c.Attributes["positive"].Value));
						tempChoice.setPositiveChance (float.Parse (c.Attributes["positivechance"].Value));
						
					}
										
						tempChoice.setDestination(System.Convert.ToInt32(c.Attributes["target"].Value));
						tempChoice.setDescription(c.InnerText);
						
					tempNode.addChoice (tempChoice);
									
				}
			}
			
			XmlNodeList triggers = n.SelectNodes ("trigger");
			
			if (triggers.Count != 0)
			{
				
				foreach (XmlNode t in triggers)
				{
			
				eventTrigger tempTrigger = new eventTrigger();
				tempTrigger.setEventType(t.Attributes["type"].Value);
				tempTrigger.setArgument (t.Attributes["argument"].Value);
				tempTrigger.setAmount(System.Convert.ToInt32 (t.Attributes["amount"].Value));
					
				tempNode.addTrigger (tempTrigger);
					
				}
			}
			
			tempEvent.addEventNode(tempNode);
									
									
		}
		
		return tempEvent;
				
		
	}
	
}
