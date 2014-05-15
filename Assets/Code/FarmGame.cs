using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmGame : MonoBehaviour {

	// Public coefficients
	public float huntingChance = 0.6f;
	public int daysBetweenHarvests = 7;
	public int daysBetweenMarkets = 14;
	public int basicDailyFoodIntake = 1;
	public int basicAnimalSellPrice = 2;
	public float basicFoodSellCost = 0.5f;
	public int farmerSalary = 1;

	// Private coefficients

	private float animalCondition = 1f;
	private int freeWorkers;
	private int huntingWorkers;
	private int harvestWorkers;
	private int animalWorkers;
	private float harvestCondition = 1f;

	// Other private things

	private int numberOfFarmers = 12;
	private int numberOfGold = 50;
	private int numberOfAnimals = 10;
	private int numberOfFood = 50;

	// Other stuff I want to expose in public (lololol)

	public int turnCounter = 1;
	public Texture2D plusButton;
	public Texture2D minusButton;
	public Texture2D goTimeButton;
	public Texture2D nextTurnButton;
	public Texture2D hunterGraphic;
	public Texture2D farmerGraphic;
	public Texture2D animalGraphic;
	public Texture2D resetButton;

	public GUIStyle numberStyle;

	// Private game flow controls etc

	private float eventChance = 0.2f;
	private bool makingChoices;
	private bool processingTurn;
	private bool runningEvents;

	private float huntedFood;
	private int huntedFoodAsInt;

	private string animalConditionAsString = "";
	private string cropConditionAsString = "";

	// Event stuff

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

		freeWorkers = numberOfFarmers;
		InitializeEvents ();


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


	// Update is called once per frame
	void Update () {

		if (!runningEvents)
		{




		}
	
	}

	void ProcessTurnEnd()
	{

		//Check to see if it's time for an event

		if (Random.Range (0f, 1f) < eventChance)
		{

			turnCounter++;
			processingTurn = false;
			DoRandomEvent ();

		}

		else 
		{
		
			turnCounter++;
			eventChance += 0.15f;
			OKToContinue ();
		}


	}

	void DoRandomEvent()
	{

		eventChance = 0.2f;
		
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
			
			numberOfAnimals += amount;
			
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
			
			numberOfAnimals -= amount;
			
			break;
		}
	}

	private void InitializeEvents()
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

		makingChoices = true;


	}

	public void StartFarmGame()
	{

		//this.numberOfFarmers = numberOfFarmers;
		freeWorkers = numberOfFarmers;
		//runningEvents = true;
		makingChoices = true;


	}

	public void OKToContinue()
	{
	
		processingTurn = false;
		animalWorkers = 0;
		huntingWorkers = 0;
		harvestWorkers = 0;
		freeWorkers = numberOfFarmers;
		makingChoices = true;
		runningEvents = false;

		//((FarmGame)GameObject.Find ("FarmGame").GetComponent ("FarmGame")).StartFarmGame (numberOfFarmers);
		
	}

	public void ProcessTurn()
	{
		huntedFood = 0f;
		makingChoices = false;

		//Hunting stuff

		for (int i = 0; i < huntingWorkers; i++)
		{

			if (Random.Range (0f, 1f) < huntingChance)
			{

				huntedFood += Random.Range (0.8f, 1.8f);
				Debug.Log ("Hunt succeeded. We have " + huntedFood + " food.");

			}

		}

		huntedFoodAsInt = (int)Mathf.Ceil(huntedFood);

		//Farming stuff. Needs to be balanced properly. Kind of a mess right now.

		harvestCondition += (harvestWorkers / 10);
		harvestCondition -= (Random.Range(0f, 0.4f));

		numberOfFood += huntedFoodAsInt;

		numberOfFood -= numberOfFarmers;

		if (harvestCondition < 0.2f)
		{

			cropConditionAsString = "almost dead";

		}

		else if (harvestCondition > 0.2f && harvestCondition < 0.5f)
		{

			cropConditionAsString = "doing poorly.";

		}

		else if (harvestCondition > 0.5f && harvestCondition < 0.8f)
		{

			cropConditionAsString = "doing quite well.";

		}

		else if (harvestCondition > 0.8f)
		{

			cropConditionAsString = "in amazing shape.";

		}


		//Animal stuff

		animalCondition += (animalWorkers / 10);
		animalCondition -= (Random.Range(0f, 0.4f));

		if (animalCondition < 0.2f)
		{
			
			animalConditionAsString = "almost dead.";
			
		}
		
		else if (animalCondition > 0.2f && animalCondition < 0.5f)
		{
			
			animalConditionAsString = "doing poorly.";
			
		}
		
		else if (animalCondition > 0.5f && animalCondition < 0.8f)
		{
			
			animalConditionAsString = "doing quite well.";
			
		}
		
		else if (animalCondition > 0.8f)
		{
			
			animalConditionAsString = "in amazing shape.";
			
		}

		processingTurn = true;


		//Housekeeping stuff. I guess these should be sent somewhere?
			    

	}

	public 

	void OnGUI()
	{

		GUI.Label (new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "HELL OF ALPHA");

		if (!runningEvents)

			if (!processingTurn)
			{

				if (makingChoices)
				{

				GUI.TextArea (new Rect(Screen.width / 2 - 100, 25, 200, 25), "Workers left: " + freeWorkers, numberStyle);

					if (GUI.Button (new Rect(Screen.width / 2 - hunterGraphic.width - 50, 100, hunterGraphic.width,
				                          hunterGraphic.height), hunterGraphic))
				    {

						if (freeWorkers > 0)
							{

								freeWorkers--;
								huntingWorkers++;


							}

					}



					if (GUI.Button (new Rect(Screen.width / 2 + 50, 100, farmerGraphic.width,
				                          farmerGraphic.height), farmerGraphic))
						{

							if (freeWorkers > 0)

								{

								freeWorkers --;
								harvestWorkers++;

								}

						}

					 if (GUI.Button (new Rect(Screen.width / 2 - hunterGraphic.width - 50, 100 + hunterGraphic.height + 50, hunterGraphic.width,
				                          hunterGraphic.height), animalGraphic))
				{

					{
						
						if (freeWorkers > 0)
							
						{
							
							freeWorkers --;
							animalWorkers++;
							
						}
						
					}


				}

					if (GUI.Button (new Rect(Screen.width / 2 - resetButton.width - 25, Screen.height - 50, 
				                          resetButton.width, resetButton.height), resetButton))
					{

						huntingWorkers = 0;
						harvestWorkers = 0;
						animalWorkers = 0;
						freeWorkers = numberOfFarmers;

					}

					if (GUI.Button (new Rect(Screen.width / 2 + 75, Screen.height - 50, goTimeButton.width, 
				                         goTimeButton.height), goTimeButton))
					{

						ProcessTurn ();
						
						//runningEvents = true;
						//DoRandomEvent ();

					}

					GUI.TextArea (new Rect(Screen.width / 2 - hunterGraphic.width / 2 - 70, 250, 32, 32), "" + huntingWorkers, numberStyle);
					GUI.TextArea (new Rect(Screen.width / 2 + 175, 250, 32, 32), "" + harvestWorkers, numberStyle);
					GUI.TextArea (new Rect(Screen.width / 2 - hunterGraphic.width / 2 - 75, 500, 32, 32), "" + animalWorkers, numberStyle);


				}

			


			}

			else if (processingTurn)
			{

				GUI.Label (new Rect(Screen.width / 2 - 200, 50, 400, 50), "At the end of turn " + turnCounter, numberStyle);

				GUI.Label (new Rect(Screen.width / 2 - 300, Screen.height / 2 - 250, 600, 50), "The hunters brought in " + huntedFoodAsInt + " food.", numberStyle);

				GUI.Label (new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 600, 50), "The workers ate " + numberOfFarmers + " food.", numberStyle);

				GUI.Label (new Rect(Screen.width / 2 - 300, Screen.height / 2 - 50, 600, 50), "We have " + numberOfFood + " food left.", numberStyle);
			
				GUI.Label (new Rect(Screen.width / 2 - 300, Screen.height / 2 + 50, 600, 50), "The animals are " + animalConditionAsString, numberStyle);

				GUI.Label (new Rect(Screen.width / 2 - 300, Screen.height / 2 + 150, 600, 50), "The crops are " + cropConditionAsString, numberStyle);

				if (GUI.Button(new Rect(Screen.width / 2 - nextTurnButton.width / 2, Screen.height - 100, 
			                        nextTurnButton.width, nextTurnButton.height), nextTurnButton))
				{

					ProcessTurnEnd();

				}


			}
		}

}
