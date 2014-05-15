using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
	
	public int numberOfSheep;
	public int numberOfFarmers;
	public int numberOfGold;
			
	public Texture2D farmStuff1;
	public Texture2D farmStuff2;
	private Texture2D selectedStuff;
	
	private bool doingFarmStuff = false;
	
	public float farmStuffInterval = 0.5f;
	private int gameEventCounter = 0;
	private float counter;
	public int farmStuffCounter = 15;
	private int repsLeft;
		
	public string[] listOfEventNames;
	
	private WinterEvent tempEvent;
	
	public float starterLikelyhood;
	
	private Object[] luettavatXML;
	
	private List<WinterEvent> starterEvents = new List<WinterEvent>();
	private List<WinterEvent> fillerEvents = new List<WinterEvent>();
	
	private List<WinterEvent> eventDeck = new List<WinterEvent>();
	
	private XMLParser soraParser = new XMLParser();
	
	// Use this for initialization
	void Start () {
				
		for (int i = 0; i < listOfEventNames.Length; i++)
		{
					
			luettavatXML = Resources.LoadAll("XML", typeof(TextAsset));
			
			//Debug.Log ("Luettiin: " +luettavatXML.Length);
			
			foreach (TextAsset t in luettavatXML)
			{
				
				//Debug.Log ("Now parsing: " + t.name);
				tempEvent = soraParser.parseXMLToEvent (t.text);
				
				if (tempEvent.getEventType() == "starter")
				{
				
					starterEvents.Add (tempEvent);
					
				}
				
				else if (tempEvent.getEventType () == "continue")
				{
				
					eventDeck.Add (tempEvent);
					
				}
				
				else {
				
					fillerEvents.Add (tempEvent);
					
				}
				
			}
						
			
			
		}
		
		selectedStuff = farmStuff1;
		repsLeft = farmStuffCounter;
		//((FarmGame)GameObject.Find ("FarmGame").GetComponent ("FarmGame")).StartFarmGame (numberOfFarmers);
		//doingFarmStuff = true;
				
	}
		
	void OnGUI()
	{
	
		GUI.Label (new Rect(50, 25, 100, 25), "Sheep: " + numberOfSheep);
		GUI.Label (new Rect(50, 45, 100, 25), "Farmers: " + numberOfFarmers);
		GUI.Label (new Rect(50, 65, 100, 25), "Gold: " + numberOfGold);
		
		if (doingFarmStuff)
		{
			
			GUI.DrawTexture (new Rect(Screen.width / 2 - 500, 200, selectedStuff.width, selectedStuff.height), selectedStuff);
									
		}
		
	}
	
	public void removeEventFromDeck(string eventname)
	{
		
			foreach (WinterEvent e in eventDeck)
		{
		
			if (e.getEventID() == eventname)
			{
				
				eventDeck.Remove (e);	
				
			}
			
		}
		
	}

	public int GetWorkers()
	{

		return numberOfFarmers;

	}
	
	public void addEventToDeck(string eventname)
	{
	
		foreach (WinterEvent e in eventDeck)
		{
		
			if (e.getEventID() == eventname)
			{
				
				eventDeck.Add (e);	
				
			}
			
		}
		
	}
	
	void DoRandomEvent()
	{
	
		if (Random.Range (0f, 1f) < starterLikelyhood)
		{
			Debug.Log ("adding storyline starter");		
			//int eventToAdd = Random.Range(0, starterEvents.Count - 1);
			
			((EventHandler)GameObject.Find ("EventHandler").GetComponent ("EventHandler")).ProcessEvent(starterEvents[Random.Range(0, starterEvents.Count - 1)]);
			
			
		}
			
		else 
		{
		
			Debug.Log ("adding random filler");
			((EventHandler)GameObject.Find ("EventHandler").GetComponent ("EventHandler")).ProcessEvent(fillerEvents[Random.Range (0, fillerEvents.Count - 1)]);
			
		}
		
	}
	
	public void OKToContinue()
	{
	
		//((FarmGame)GameObject.Find ("FarmGame").GetComponent ("FarmGame")).StartFarmGame (numberOfFarmers);
		
	}
	
	public void addResource(string resourceType, int amount)
	{
	
		switch (resourceType)
		{
		
		case "farmer":
		
			numberOfFarmers += amount;
		
		break;
			
		case "gold":
		
			numberOfGold += amount;
			
		break;
		
		case "sheep":
		
			numberOfSheep += amount;
		
		break;
		}
	}
	
	public void removeResource(string resourceType, int amount)
	{
	
		switch (resourceType)
		{
		
		case "farmer":
		
			numberOfFarmers -= amount;
		
		break;
			
		case "gold":
		
			numberOfGold -= amount;
			
		break;
		
		case "sheep":
		
			numberOfSheep -= amount;
		
		break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (doingFarmStuff)
		{
		
			counter += Time.deltaTime;
			//Debug.Log (counter);
			
			if (counter > farmStuffInterval)
			{
			
				counter = 0;
				
				//Debug.Log("Reps left" + repsLeft);
				
				if (selectedStuff == farmStuff1)
				{
					//Debug.Log ("Break1");
					selectedStuff = farmStuff2;
					repsLeft--;
					
				}
				
				else if (selectedStuff == farmStuff2)
				{
				
					//.Log ("Break2");
					selectedStuff = farmStuff1;
					repsLeft--;
					
				}
												
				if (repsLeft == 0)
				{
				
					repsLeft = farmStuffCounter;
					counter = 0f;
					doingFarmStuff = false;
					DoRandomEvent ();
					
				}
				
				
			}
			
		}
	
	}
}
