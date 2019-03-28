using Godot;
using System;

public class UserInput : Godot.Node
{
    public session currentSession { get; set; }
    public override void _Ready()
    {

    }
    public void cardGotClicked(Node where, baseCard what)
    {
        if(where is player_hand)
        {

            GD.Print(what, " got clicked in player hand");
            if (currentSession.currentPhase == session.Phase.PLAY && where.Name == "player_hand")
            {
                //hardcode to player1
                currentSession.player.Rpc("handToField",what.cardId);
            }
        }
    }
}