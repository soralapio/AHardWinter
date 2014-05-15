using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerProcessor : MonoBehaviour {
	
	private List<eventTrigger> triggersToProcess = new List<eventTrigger>();
	private string currentType;
	private string currentArgument;
	private int currentAmount;
	private bool processingTriggers;
	private bool readyForNext = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (processingTriggers)
		{
			if (readyForNext == true && triggersToProcess.Count != 0)
			{
				readyForNext = false;	
				processTrigger(triggersToProcess[0]);
				
			}
			
			
		}
		
	}
	
	public void addTriggerToProcess(eventTrigger trigger)
	{
		Debug.Log ("Received new trigger.");
		
		if (triggersToProcess.Contains (trigger) == false)
		{
		
			triggersToProcess.Add (trigger);
			if (processingTriggers == false)
			{
				Debug.Log ("Trigger accepted.");
				
				processingTriggers = true;	
				
			}
			
		}
		
	}
	
	private void processTrigger(eventTrigger trigger)
	{
		Debug.Log("Processing trigger.");
		currentType = trigger.getEventType ();
		currentArgument = trigger.getArgument ();
		currentAmount = trigger.getAmount ();
		
		switch (currentType)
		{
			
			case "sendmessage":
				
			Debug.Log ("Message received: " + currentArgument);
			
			triggersToProcess.Remove (trigger);
			readyForNext = true;
			break;
				
						
			case "removeresource":
			
			((FarmGame)GameObject.Find("FarmGame").GetComponent ("FarmGame")).removeResource (currentArgument, currentAmount);
			triggersToProcess.Remove (trigger);
			readyForNext = true;
			
			break;
			
			case "addresource":
			
			((FarmGame)GameObject.Find("FarmGame").GetComponent ("FarmGame")).addResource (currentArgument, currentAmount);
			triggersToProcess.Remove (trigger);
			readyForNext = true;
			
			break;
			
			case "addevent":
			
			((FarmGame)GameObject.Find ("FarmGame").GetComponent ("FarmGame")).addEventToDeck (currentArgument);
		
			break;
			
			case "removeevent":
			
			((FarmGame)GameObject.Find ("FarmGame").GetComponent ("FarmGame")).removeEventFromDeck (currentArgument);
			
			break;
			
		}
		
		
	}
}
