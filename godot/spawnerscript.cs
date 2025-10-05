using Godot;
using System;

public partial class spawnerscript : PathFollow2D
{

	bool spawnerOverheating = true;

	float timervalue = 1f;
	float timer;

	[Export]
    public PackedScene objectprefab { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

        //after a timer runs out, go to a random part of progressratio and spawn an object


		timer -= 1 * (float)delta;

        if (timer <= 0 && spawnerOverheating == true)
        {
            spawnerOverheating = false;
        }

        if (spawnerOverheating == false)
		{
			objectscript objecttospawn = objectprefab.Instantiate<objectscript>();


			ProgressRatio = GD.Randf();

			objecttospawn.GlobalPosition = GlobalPosition; //assigns its initial spawn position to the spawner's location. not doing this makes it spawn on the location of the main node

			Owner.AddChild(objecttospawn); //adds it as a child of the main node so its not chained to the rapidly changing position of the spawner



			timer = timervalue; //reset timer

			spawnerOverheating = true;
		}

		
	}

}
