using Godot;
using System;
using System.Collections.Generic;


public class data_man : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    
    // its shit public Godot.Collections.Dictionary<string, string> allCards;
    //public Dictionary<int, card> allCards;
    public List<card> allCards =  new List<card>();
    [Export] public Resource element0;
    [Export] public Resource element1;
    [Export] public Resource element2;
    [Export] public Resource element3;
    [Export] public Resource element4;
    [Export] public Resource element5;
    [Export] public Resource element6;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //this is needed because it seems C# doesn't like JSON
        GetNode("GD_data_man").Call("send_data");
    }


    public void loadJson(string filePath)
    {
        var file = new File();
        file.Open(filePath, 1);
        string text = file.GetAsText();
        JSONParseResult dict = JSON.Parse(text);
        file.Close();

        if (dict.Error != 0)
        {
            GD.Print("ErrorString:", dict.ErrorString);
            GD.Print("ErrorType:", dict.Error);
            GD.Print("ErrorLine:", dict.ErrorLine);
        }
        else
        {
            var parsed = new Godot.Collections.Dictionary<object, object>();

            if (dict.Result is Godot.Collections.Dictionary)
            {
                
                
                //this does somehow not work and always returns null
                //thats why I wrote a parser in GDscript and then send it over
                parsed = (Godot.Collections.Dictionary<object, object>)dict.Result;
                GD.Print("parsed: ");
                GD.Print(parsed);
            }
        }
    }
    public card resolveCardId(int id)
    {
        if(allCards[id] != null)
        {
            return allCards[id];
        }
        else
        {
            return allCards[0];
        }
    }
    public Texture getCardPicture(int element)
    {
        switch(element)
        {
            case 0:
                return (Texture)element0;
            case 1:
                return (Texture)element1;
            case 2:
                return (Texture)element2;
            case 3:
                return (Texture)element3;
            case 4:
                return (Texture)element4;
            case 5:
                return (Texture)element5;
            default:
                return (Texture)element0;

        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void addCard(int id, string name, int type, int element)
    {   
        allCards.Add(item: init_card(id, name, type, element));
    }
    private card init_card(int id, string name, int type, int element)
    {
        var card = new card();
        card.id = id;
        card.name = name;
        card.type = type;
        card.element = element;
        return card;
    }

}

public class card
{
    public int id;
    public string name;
    public int type;
    public int element;

    public void init(int id, string name, int type, int element)
    {
        this.name = name;
        this.id = id;
        this.type = type;
        this.element = element;
    }  
    
    public string toString()
    {
        string stringified = "card: ";
        stringified += name;
        return stringified;
    }
}
