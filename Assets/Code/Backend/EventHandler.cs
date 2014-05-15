using UnityEngine;
using System.Collections.Generic;

public class EventHandler : MonoBehaviour {
	
	
	public GUIStyle headerStyle;
	public GUIStyle normalStyle;
	public GUIStyle buttonStyle;
	
	public Texture2D titleBG;
	public Texture2D bodyBG;
	public Texture2D buttonBG;
	
	private bool areYouProcessing = false;
	private WinterEvent eventToProcess;
	private int nextNodeToProcess = 0;
	private string eventName;
	private string nodeText;
	private eventNode currentEvent;
	private List<eventChoice> currentChoices = new List<eventChoice>();
	private List<eventTrigger> currentTriggers;
	private bool playerInteraction = true;
		

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (areYouProcessing)
		{
									
			
		}
	
	}
	
	
	void OnGUI()
	{
		
		if (areYouProcessing)
		{   
			GUI.DrawTexture (new Rect(Screen.width / 2 - 300, Screen.height / 2 - 250, titleBG.width, titleBG.height), titleBG);
			GUI.Label (new Rect(Screen.width / 2 - 290, Screen.height /2 - 250, 580, 50), eventName, headerStyle);
			GUI.DrawTexture (new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, bodyBG.width, bodyBG.height), bodyBG);
			GUI.Label (new Rect(Screen.width / 2 - 290, Screen.height / 2 - 200, 580, 300), nodeText, normalStyle);
			
			if (currentChoices.Count != 0)
			{
				for (int i = 0; i < currentChoices.Count; i++)
				{
				
				GUI.DrawTexture (new Rect(Screen.width / 2 - 300, Screen.height / 2 + 100 + (25 * i), 600, 25), buttonBG);
				if (GUI.Button (new Rect(Screen.width /2 - 300, Screen.height / 2 + 100 + (25 * i), 600, 25), currentChoices[i].getDescription(), buttonStyle))
					{
						if (playerInteraction == true)
						{	
							Debug.Log ("Jumping to node: " + currentChoices[i].getDestination ());
							AccessNode(currentChoices[i].getDestination ());
						}
						
					}				}			
				
			}
			
			else
				
			{
			GUI.DrawTexture (new Rect(Screen.width / 2 - 300, Screen.height / 2 + 100, 600, 25), buttonBG);
			if (GUI.Button (new Rect(Screen.width / 2 - 300, Screen.height / 2 + 100, 600, 25), "Next.", buttonStyle))
				{
					
					GameObject.Find ("FarmGame").GetComponent ("FarmGame").SendMessage ("OKToContinue");
					areYouProcessing = false;
					
				}
			}
		}
		
	}
	
	private void AccessNode(int nodeindex)
	{
		currentChoices.Clear();
		//Debug.Log ("Seuraavaksi halutaan node " + nodeindex);
		playerInteraction = false;
		currentEvent = eventToProcess.getEventNode(nodeindex);
		//Debug.Log (currentEvent.ToString ());
		nodeText = currentEvent.getEventText ();
		if (currentEvent.checkChoices() != false)
		{
			
			currentChoices = currentEvent.getEventChoices();
			//Debug.Log ("Choiceja ladattiin " + currentChoices.Count);
			
		}
		
		if (currentEvent.gotTriggers() != false)
		{
		
			List<eventTrigger> tempTriggers = currentEvent.getTriggers ();
			foreach (eventTrigger t in tempTriggers)
			{
				
				((TriggerProcessor)GameObject.Find ("TriggerProcessor").GetComponent ("TriggerProcessor")).addTriggerToProcess (t);
				
			}
		}
		
		playerInteraction = true;
		
		
	}
	
	public void ProcessEvent(WinterEvent eventToProcess)
	{
		//Debug.Log (eventToProcess.ToString());
		this.eventToProcess = eventToProcess;
		eventName = eventToProcess.getEventName();
		currentEvent = eventToProcess.getEventNode(0);
		nodeText = currentEvent.getEventText();
		if (currentEvent.checkChoices() != false)
		{
			
			currentChoices = currentEvent.getEventChoices();
			//Debug.Log ("Choiceja ladattiin " + currentChoices.Count);
			
		}
			
		
		areYouProcessing = true;
		
	}
}
