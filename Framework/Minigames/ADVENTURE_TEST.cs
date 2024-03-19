using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks.Dataflow;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame1 : MinigameDefBase{
    public override string BackgroundImage {get; set;} = "/images/FIGHTER.png";



public Rectangle? Rect {get; set;}
public Rectangle? Rect1 {get; set;}
public GameObjectContainer<Rectangle> buttons {get; set;} = new(); //pokemon UI 
public GameObjectContainer<Rectangle> moving_rects {get; set;} = new(); //bilder 
public GameObjectContainer<Rectangle> decoration {get; set;} = new();


public MyMinigame1(){
    Rect = new(){
        Id = "2ADSFG",
        X= 400,
        Y= 800,
        Width= 300,
        Height= 100,
        Fill="blue",
        // OnClick = (args) => moving_rects.Transform(async (R) => {
        //     R.Y += 5; 
        //     Update(); 
        //     await Task.Delay(50); 
        //     R.Y -= 5;
        //     Update(); })
        OnClick = async (args) => {
            Rect.Y += 5;
            Update();
            await Task.Delay(50); 
            Rect.Y -= 5;
            Update();
        }

       
    };
    moving_rects.Add(Rect);
    AddElement(Rect);



   Rect1 = new(){
        Id = "rectbutton55",
        X= 800,
        Y= 800,
        Width= 300,
        Height= 100,
        Fill="blue",
        OnClick = (args) => moving_rects.Transform((R) => {R.Y += 10; Update();})
       
    };
    moving_rects.Add(Rect1);
    AddElement(Rect1);
    
}
/*
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