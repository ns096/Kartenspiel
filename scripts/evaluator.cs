using Godot;
using System;

public class evaluator : Node
{
    //this is the class for effects that happen in game
    //it evaluates card effects and controls game events
    //it also evaluates the elematrix 

    [Signal]delegate void DataHasChanged(player_data player);
    public data_man dataMan;
    public player_data player1;
    public player_data player2;
    

    public override void _Ready()
    {
        
    }
    //this should be called by parent
    //or else the references fail
    public void setupDependencies(player_data player1, player_data player2, data_man dataMan)
    {
        this.player1 = player1;
        this.player2 = player2;
        this.dataMan = dataMan;

        GetParent().Connect("EventGameStart", this, "gameStart");
        GetParent().Connect("EffectDrawCardFromDeck", this, "drawFromDeck");
        player1.Connect("DataHasChanged",this, "dataHasChanged", new Godot.Collections.Array {player1});
        player2.Connect("DataHasChanged",this, "dataHasChanged", new Godot.Collections.Array {player2});
    }

    //connects to player_data and draws the amount of cards
    //gets called by session.cs or when a card effect or game event has happened
    [Master]
    public void drawFromDeck(player_data player, int amount)
    {
        player.drawFromDeck(amount);
    }
    
    [Sync]
    public void dataHasChanged(player_data player)
    {
        EmitSignal("DataHasChanged", player);
    }

    //fires on GameStart Signal
    public void gameStart()
    {
        GD.Print("GameStarted");
        player1.Rpc("setupDeck");
        player2.Rpc("setupDeck");
        Rpc("drawFromDeck",player1, 3);
        Rpc("drawFromDeck",player2, 3);
    }

}
