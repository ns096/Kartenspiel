using Godot;
using System;

public class render_man : Spatial
{

    //basis scene for a card
    [Export] public PackedScene baseCard;
    public data_man dataMan;
    public session session;
    private player_hand playerHand;
    private player_hand opponentHand;
    private game_field gameField;
    public Random rand;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //dataMan = (data_man)GetParent().GetNode("data_man");
        rand = new Random();
        setupDependencies();
    }

    //setup highlevel to lowlevel dependencies
    //without too much strong coupling
    private void setupDependencies()
    {
        session = (session)GetParent();
        playerHand = GetNode<player_hand>("player_hand");
        opponentHand = GetNode<player_hand>("opponent_hand");
        gameField = GetNode<game_field>("game_field");
    }
    //gets called by session after dataHasChangedSignal from player_data
    //takes the current state of player_data
    //and sends it to all modules responsible for rendering those on screen
    public void updatePlayerCards(player_data playerData)
    {
        var playerRole = resolvePlayerRole(playerData);
        //update hand cards
        updatePlayerHand(playerRole, playerData);
        //update gamefield
        updatePlayerField(playerRole, playerData);

    }

     public void updatePlayerHand(string playerRole, player_data player)
    {
        //update playerHand depending on player1 = player or player2 = opponent
        if (playerRole == "player")
        {
            playerHand.emptyHand();
            foreach (int cardId in player.cardsInHand)
            {
                playerHand.addCardToHand(initNewCard(cardId));
            }
        }
        else
        {
            opponentHand.emptyHand();
            foreach (int cardId in player.cardsInHand)
            {
                opponentHand.addCardToHand(initNewCard(cardId));
            }
        }

    }

    public void updatePlayerField(string playerRole, player_data player)
    {
        //reset current playerField
        gameField.emptyField(player);
        //update playerField depending on player1 = player or player2 = opponent

        //renderman is responsible over player_data assignment to corresponding game slots
        if (playerRole == "player")
        {
            foreach (int cardId in player.cardsOnField)
            {
                var card = initNewCard(cardId);
                gameField.updateSlot(playerRole,"slot_1",card);
            }
        }
        else
        {
            foreach (int cardId in player.cardsOnField)
            {
                var card = initNewCard(cardId);
                gameField.updateSlot(playerRole,"slot_1",card);
            }
        }
    }

    
   

    //resolve role of player1 and player2 on this client
    public string resolvePlayerRole(player_data player)
    {
        return player.playerRole;
    }
    

    //TEST input purposes
    //UserInput singleton should be used
    public override void _Input(InputEvent @event)
    {
        //TEST baseCard functionality with spawning one directly
        if (Input.IsActionPressed("ui_down"))
        {
            session.EmitSignal("EffectDrawCardFromDeck", session.player, 1);
        }
    }

    //instantiate a new baseCard 
    //and provide cardId dependent look
    //takes the cardId and looks it up in the dataMan
    //dataMan is responsible for the textures 
    public baseCard initNewCard(int cardId)
    {
        var newCard = (baseCard)baseCard.Instance();
        newCard.cardId = cardId;
        var card = dataMan.resolveCardId(newCard.cardId);
        newCard.pictureTexture = dataMan.getCardPicture(card.element);
        return newCard;
    }

}
