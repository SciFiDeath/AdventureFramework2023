namespace Room.Utils;

/*
Button struct is used to define a button in a room.

* Id: The id of the button. Must be unique in the room the button is in.
* Points: The points of the button. Must be a valid SVG polygon points string.
* Action: The action of the button. Must be a valid action string.
Valid actions are: 
	* "route": Change to a different room/slide. Args: {"internal/external", "roomId/slideId"}.
	* "inventory": Add/remove item from inventory. Args: {"itemId", "amount" (negative for removing)}.
	* "sound": Play a sound. Args: {"soundId", "some stuff to be added when we actually implement it"}.
* Args: The arguments of the button. Must be a valid action arguments string array
*/
public readonly struct Button
{
	public readonly string Id;
	public readonly string Points;
	public readonly string Action;
	public readonly string[] Args;
	
	public Button(string Id, string Points, string Action, string[] Args)
	{
		this.Id = Id;
		this.Points = Points;
		this.Action = Action;
		this.Args = Args;
	}
	
	public Signal GetSignal()
	{
		return new Signal(Action, Args);
	}
}

/*
Bascially a button without the id and points.
*/
public readonly struct Signal 
{
	public readonly string Action;
	public readonly string[] Args;
	
	public Signal(string Action, string[] Args)
	{
		this.Action = Action;
		this.Args = Args;
	}
}	