using Godot;
using System;

public class Test_1 : Node
{
    private Control Console;
    private KinematicBody Player;

    public override void _Ready()
    {
        Console = GetNode<Control>("Control");

        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
