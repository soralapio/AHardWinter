using System.Collections;

public class eventTrigger{
	
	private string eventType;
	private string argument;
	private int amount;
	
	public eventTrigger(string eventType, string argument, int amount)
	{
	
		this.eventType = eventType;
		this.argument = argument;
		this.amount = amount;
		
	}
	
	public eventTrigger()
	{
	
		
	}
	
	public void setEventType(string eventType)
	{
	
		this.eventType = eventType;
		
	}
	
	public string getEventType()
	{
		
		return eventType;
		
	}
	
	public void setArgument (string argument)
	{
	
		this.argument = argument;
		
	}
	
	public string getArgument()
	{
		
		return argument;
		
	}
	
	public void setAmount(int amount)
	{
	
		this.amount = amount;
		
	}
	
	public int getAmount()
	{
	
		return amount;
		
	}
	
}
