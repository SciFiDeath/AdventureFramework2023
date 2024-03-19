using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks.Dataflow;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame1 : MinigameDefBase{
    public override string BackgroundImage {get; set;} = "/images/calculator.png";

/*
[Element]
public Rectangle Rect {get; set;}
public Rectangle Rect1 {get; set;}
public GameObjectContainer<Rectangle> movingrects {get; set;} = new();
public GameObjectContainer<Rectangle> rects {get; set;} = new();

public MyMinigame1(){
    Rect1 = new(){
        Id = "rectbutton",
        X= 400,
        Y= 50,
        Width= 200,
        Height= 50,
        Fill="blue",
        OnClick = SPAWNER,
       
    };
    AddElement(Rect1);
    rects.Add(Rect1);
    
}
public void SPAWNER(EventArgs args){
    var rand = new Random();
    Rect = new(){
        Id = "rect",
        X=rand.Next(1,1500),
        Y=100,
        Width=100,
        Height=100,
        Fill="red",
        OnClick= (args) => { movingrects.Transform((movingrects) => movingrects.Visible = !movingrects.Visible); Update(); }
    };
    movingrects.Add(Rect);
    AddElement(Rect);
    Update();
}

public override async Task GameLoop(CancellationToken token){
    rects.Add(Rect1);
    while(true){
        
        if(token.IsCancellationRequested){
            return;
        }
        
        movingrects.Transform((rect) => rect.Y +=5);
        Update();
        await Task.Delay(10,token);
        
    }
}*/

}