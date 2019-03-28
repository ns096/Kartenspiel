using Godot;
using System;

public class baseCard : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public int cardId;
    public MeshInstance cardPicture;
    public Texture pictureTexture;

    [Signal] delegate void leftClicked(baseCard itself);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardPicture = GetNode<MeshInstance>("picture");
        //pictureTexture = (Texture)ResourceLoader.Load("res://textures/types/type2.png");
        InitCard(pictureTexture);
        GetNode("click_area").Connect("input_event", this, "_OnClick");
    }
    public void InitCard(Texture Texture)
    {
        if (cardPicture.GetSurfaceMaterial(0) is SpatialMaterial){
            var uniqueMaterial = (SpatialMaterial)cardPicture.GetSurfaceMaterial(0).Duplicate();
            SpatialMaterial cardPictureMaterial = uniqueMaterial;
            uniqueMaterial.AlbedoTexture = pictureTexture;
            cardPicture.SetSurfaceMaterial(0, uniqueMaterial);
        }
    }
    public void _OnClick(Node camera, InputEvent Event, Vector3 click_position, Vector3 click_normal, int shape_idx)
    {
        if(Event.IsActionReleased("right_click"))
        {
            GD.Print("right lciked");
            
        }
        else if(Event.IsActionReleased("left_click"))
        {
            GetParent().Call("cardGotClicked", this );
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}