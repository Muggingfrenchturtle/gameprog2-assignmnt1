using Godot;
using System;

public partial class objectscript : Area2D
{
	public float shootForce = 90;
    private CollisionShape2D playerhitbox;
	private Vector2 objectforce = Vector2.Zero;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		playerhitbox =  GetNode<CollisionShape2D>("/root/main/playerdial/playerhitbox");

        

        //find location of player
        objectforce.X = ((playerhitbox.GlobalPosition.X - GlobalPosition.X) / (float)Math.Sqrt(Math.Pow(playerhitbox.GlobalPosition.X - GlobalPosition.X, 2) + Math.Pow(playerhitbox.GlobalPosition.Y - GlobalPosition.Y, 2))) * shootForce;

        objectforce.Y = ((playerhitbox.GlobalPosition.Y - GlobalPosition.Y) / (float)Math.Sqrt(Math.Pow(playerhitbox.GlobalPosition.X - GlobalPosition.X, 2) + Math.Pow(playerhitbox.GlobalPosition.Y - GlobalPosition.Y, 2))) * shootForce;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        

        /*
        putting the related code in process instead of ready results in full tracking
        Vector2 objectforce = Vector2.Zero;

        objectforce.X = ((playerhitbox.GlobalPosition.X - GlobalPosition.X) / (float)Math.Sqrt(Math.Pow(playerhitbox.GlobalPosition.X - GlobalPosition.X, 2) + Math.Pow(playerhitbox.GlobalPosition.Y - GlobalPosition.Y, 2))) * shootForce;

        objectforce.Y = ((playerhitbox.GlobalPosition.Y - GlobalPosition.Y) / (float)Math.Sqrt(Math.Pow(playerhitbox.GlobalPosition.X - GlobalPosition.X, 2) + Math.Pow(playerhitbox.GlobalPosition.Y - GlobalPosition.Y, 2))) * shootForce;
        */
        //then move towards it
        GlobalPosition += objectforce * (float)delta;
	}
}
