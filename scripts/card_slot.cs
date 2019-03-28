using Godot;
using System;

public class card_slot : Spatial
{

    public override void _Ready()
    {
        
    }

    public void cardGotClicked(baseCard clickedCard)
    {
        var userInput = GetNode<UserInput>("/root/UserInput");
        GD.Print("hand: ", clickedCard, " got clicked");
        userInput.CallDeferred("cardGotClicked", this, clickedCard);
        
    }


}
