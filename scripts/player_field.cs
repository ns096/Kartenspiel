using Godot;
using System;

public class player_field : Spatial
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void emptySlots()
    {
        foreach(Node slot in GetNode("slots").GetChildren())
        {
            
            //iterating and removing elements with foreach is iffy
            //this way the numbers are fixed while runtime
            var cardsInSlot = slot.GetChildren();
            int childCount =  slot.GetChildCount();
            for (int i = 1; i <= childCount; i++)
            {
                var slotCard = cardsInSlot[i-1] as Node;
                RemoveChild(slotCard);
                slotCard.QueueFree();
            }
        
        }
    }

    public void playCardOnField(baseCard card)
    {
        GetNode("slots").GetNode("slot1").AddChild(card);
    }

    public void playCardOnSubjugator(baseCard card)
    {
        GetNode("slots").GetNode("slot2").AddChild(card);
    }

    public void updateSlots(player_data player)
    {
        //player.cardsOnField
    }


}
