
namespace Framework.Minigames.MinigameDefClasses;

public class DimitriDialogue : MinigameDefBase
{

    public override string BackgroundImage {get; set;} = "images/HM3_hallwayN.jpg"; // Background Image


	//DIALOGUE STUFF
	private Rectangle QuitButton { get; set;}
	private Rectangle ForwardButton { get; set;}

	private Dialogue dialogue;
	
	//MINIGAME STUFF
	[Element]
	public required Rectangle NpcHitBox { get; set;} 
	
	List<List<string>> messages = [
		["player", "Hello"],
		["npc", "bring me something"],
		["player", "Hello"],
		["npc", "did you bring it?"],
		["npc", "this text is white"]

	];

	public DimitriDialogue(){
		
		dialogue = new Dialogue(messages);

		NpcHitBox = new(){
			X = 1500,
			Y = 1000,
			Width = 100,
			Height = 100,
			Fill = "green",
			OnClick = async (args) => {Console.WriteLine("Click On NPC"); await StartDialogueAsync();}
		};

	}

	public async Task StartDialogueAsync(){
		
		bool quit = false;
		bool forward = false;

		//Create forward and quit button
		QuitButton = dialogue.DrawQuitButton();
		ForwardButton = dialogue.DrawForwardButton();

		AddElement(QuitButton);
		AddElement(ForwardButton);

		QuitButton.OnClick = (args) => {quit = true; };
		ForwardButton.OnClick = (args) => {forward = true; };

		Update();


		foreach (List<string> speech in messages){
			
			GameObjectContainer<SVGElement> Bubble = dialogue.DrawSpeechBubble(speech[0], speech[1]);
			
			AddElementsInContainer(Bubble);

			Update();


			//Task.Delay(5000);
			await WaitForConditionAsync(() => forward || quit);

			
			foreach (string key in Bubble.Keys){
				Elements.Remove(key);
				Bubble.KillId(key);
			}

			if (quit == true){
				break;
			}
			quit = false;
			forward = false;

			Update();
		}

	}

	private async Task WaitForConditionAsync(Func<bool> condition)
{
    while (!condition())
    {
        await Task.Delay(100); // Check the condition every 100 milliseconds
    }
}



}