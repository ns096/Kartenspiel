using Godot;
using System;

public class player_hand : Spatial
{

    private const float gap = 2.7f;
    private session session;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        session = GetParent().GetParent<session>();
    }

    public void emptyHand()
    {
        //iterating and removing elements with foreach is iffy
        //this way the numbers are fixed while runtime
        var handCards = GetChildren();
        int childCount =  GetChildCount();
        for (int i = 1; i <= childCount; i++)
        {
            var handCard = handCards[i-1] as Node;
            RemoveChild(handCard);
            handCard.QueueFree();
        }
    }

    public void addCardToHand(baseCard newCard)
    {
        AddChild(newCard);
        arrangeHandCards();
    }

    //TODO don't throw away all cards
    //instead only add or substract the changed card
    public void updateCards(baseCard[] newCards)
    {
        foreach(baseCard card in newCards)
        {

        }
    }


    //arrange the cards so that they always stay centered
    //multiply the amount times the prefered gap
    //and move the whole thing half that ammount to the lefthand side
    private void arrangeHandCards()
    {
        var handCards = GetChildren();
        for (int i = 1; i <= GetChildCount(); i++)
        {
            var handCard = handCards[i-1] as Spatial;
            var handCardTransform = Transform;
            handCardTransform.Translated(new Vector3(x: -(gap*GetChildCount())/2+(i*gap), y:0, z:0));
            handCard.Transform = handCardTransform;
            
        }
    }

    //input handling of baseCard Child
    public void cardGotClicked(baseCard clickedCard)
    {
        var userInput = GetNode<UserInput>("/root/UserInput");
        GD.Print("hand: ", clickedCard, " got clicked");
        if(session.currentPhase == session.Phase.PLAY)
        {
            userInput.CallDeferred("cardGotClicked", this, clickedCard);
        }
    }
}
