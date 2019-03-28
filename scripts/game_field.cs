using Godot;
using System;

public class game_field : Spatial
{

    private player_field opponentField;
    private player_field playerField;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        setupDependencies();
    }

    private void setupDependencies()
    {
        playerField = GetNode<player_field>("player");
        opponentField = GetNode<player_field>("opponent");
    }
    public void emptyField(player_data player)
    {
        if(player.playerRole == "player")
        {
            playerField.emptySlots();
        }
        else
        {
            opponentField.emptySlots();
        }
    }

    //takes playerRole and corresponding slot and assign card
    //game field is not responsible about how player_data gets assigned
    public void updateSlot(string player, string slot, baseCard card)
    {
        GetNode(player).GetNode("slots").GetNode(slot).AddChild(card);
    }
}
