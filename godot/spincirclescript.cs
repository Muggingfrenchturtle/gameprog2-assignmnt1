using Godot;
using System;

public partial class spincirclescript : Area2D
{
	public float spinSpeed = 100;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotationDegrees += spinSpeed * (float)delta;

		if (Input.IsActionJustPressed("Space"))
		{
			spinSpeed *= -1; //makes current spinspeed negative
		}

		
	}
}
