using Godot;
using System;

public class session : Spatial
{
    [Signal] public delegate void EventGameStart();
    [Signal] public delegate void EffectDrawCardFromDeck(player_data player, int amount);
    [Signal] public delegate void EventGameFinished();

    public enum Phase {DRAW, PLAY, EVALUATE, OVER};
    public Phase currentPhase;

    public player_data player;
    public player_data opponent;
    public evaluator evaluator;
    private UserInput userInput;
    private render_man renderMan;
    public Random rng;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        setupForMultiplayer();
        setupDependencies();
        
        rng = new Random();
        currentPhase = Phase.DRAW;
    }

    void setupDependencies()
    {
        //give render_man access to data_man
        renderMan = GetNode<render_man>("render_man");
        evaluator = GetNode<evaluator>("evaluator");

        evaluator.setupDependencies(GetNode<player_data>("player1"), GetNode<player_data>("player2"), (data_man)GetNode("data_man"));
        renderMan.dataMan = GetNode("data_man") as data_man;
        evaluator.Connect("DataHasChanged", this, "playerDataHasChanged");
        var userInput = GetNode<UserInput>("/root/UserInput");
        userInput.currentSession = this;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
        switch (currentPhase) {
            case Phase.DRAW : drawPhase(); break;
            case Phase.PLAY : playPhase(); break;
            case Phase.EVALUATE : evaluatePhase(); break;
            case Phase.OVER : overPhase(); break;
            default : break;
        }
    }

    void drawPhase() 
    {
        // Business logic
        GD.Print("DrawPhase");
        EmitSignal("EventGameStart");
        currentPhase = Phase.PLAY;
    }

    void playPhase() 
    {
        // Business logic
        currentPhase = Phase.PLAY;
    }

    void evaluatePhase() 
    {
        // Business logic
        currentPhase = Phase.DRAW;
    }

    void overPhase() 
    {
        // Business logic
        currentPhase = Phase.PLAY;
    }

    //on signal dataHasChanged from player_data #endregion
    //update the current rendered cards
    //of the corresponding player
    
    void playerDataHasChanged(player_data player)
    {
        renderMan.updatePlayerCards(player);
    }

    public void setupForMultiplayer()
    {
        //by default, all nodes in server inherit from master
	    //while all nodes in clients inherit from slave
        if (GetTree().IsNetworkServer())
        {
            //if in the server, give control of player 2 to the other peer, this function is tree recursive by default
            GetNode("player2").SetNetworkMaster(GetTree().GetNetworkConnectedPeers()[0]);
            opponent = GetNode<player_data>("player2");
            opponent.playerRole = "opponent";
            player = GetNode<player_data>("player1");
            player.playerRole = "player";
        }		  
        else if(!GetTree().IsNetworkServer())
        {
            //if in the client, give control of player 2 to itself, this function is tree recursive by default
            GetNode("player2").SetNetworkMaster(GetTree().GetNetworkUniqueId());
            GetNode("render_man").SetNetworkMaster(GetTree().GetNetworkUniqueId());
            opponent = GetNode<player_data>("player1");
            opponent.playerRole = "opponent";
            player = GetNode<player_data>("player2");
            player.playerRole = "player";
        }
        else
        {
            //no network so standard stuff
            //player2.SetNetworkMaster(GetTree().GetNetworkUniqueId());
            opponent = GetNode<player_data>("player2");
            opponent.playerRole = "opponent";
            player = GetNode<player_data>("player1");
            player.playerRole = "player";
        }

        // let each paddle know which one is left, too
        // get_node("player1").left=true
        // get_node("player2").left=false
        // print("unique id: ",get_tree().get_network_unique_id())
    }
    

}
