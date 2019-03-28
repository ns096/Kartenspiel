using Godot;
using System;
using System.Collections.Generic;

public class player_data : Node
{
    public string playerRole;

    //complete sorted decklist
    private List<int> fullDeckList = new List<int>();
    //cards currently in deck
    public List<int> cardsInDeck;
    //cards currently in hand
    public List<int> cardsInHand = new List<int>();
    //cards currently in discard pile
    public List<int> cardsInDiscard = new List<int>();
    //cards that have won and have been placed on the subjugator field
    public List<int> cardsOnSubjugator = new List<int>();
    //card gets played from hand waits there for evaluation
    //so either lands on subjugator or discard
    public List<int> cardsOnField = new List<int>();

    [Signal] public delegate void DataHasChanged();

    private Random rng;



    public override void _Ready()
    {
        
        
    }
    //called by evaluator at gamestart
    [Master]
    public void setupDeck()
    {
        rng = new Random();
        fillDeck();
        shuffleDeck();
    }

    void loadDeck()
    {
        //GetParent().GetNode("GD_data_man").Call("load_json_file", "res://playerDeck.json");
    }
    //fill the deck with cardId 1-4
    //for the test purposes
    [Master]
    public void fillDeck()
    {
        for (int i = 0; i < 20; i++)
        {
            fullDeckList.Add(((int)i / 4)+1);
        }
        cardsInDeck = new List<int>(fullDeckList);
    }
    //take card from current cards In Deck
    //add it to player hand and remove it from cards in deck
    [Master]
    public void drawFromDeck(int amount)
    {
        if (amount > cardsInDeck.Count)
        {
            amount = cardsInDeck.Count;
        }
        for (int i = 0; i < amount; i++)
        {
            cardsInHand.Add(cardsInDeck[i]);
        }
        
        cardsInDeck.RemoveRange(0, amount);
        EmitSignal("DataHasChanged");

    }
    public void shuffleDeck() => Shuffle(cardsInDeck);

    
    //first return played card to hand and then empty it
    //then take cardId in hand and place it in cardsOnField
    [Sync]
    public void handToField(int cardId)
    {
        if(cardsInHand.Exists(item => item == cardId))
        {
            if(cardsOnField.Count != 0)
            {
                foreach(int card in cardsOnField)
                {
                    cardsInHand.Add(card);
                }
                cardsOnField.Clear();
            }
            cardsOnField.Add(cardId);
            cardsInHand.Remove(cardId);
        }
        EmitSignal("DataHasChanged");
    }
    
    public void remove(List<int> where, int cardId)
    {
        if(where.Exists(item => item == cardId))
        {
            where.Remove(cardId);
        }
    }

    public void Shuffle(List<int> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
}

    
}
