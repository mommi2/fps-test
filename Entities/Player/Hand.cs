using Godot;
using System;

public class Hand : Spatial
{
    
    
    public override void _Ready()
    {
        M16 m16 = ResourceLoader.Load<PackedScene>("res://Entities/Weapons/Guns/Rilfes/M16/Rifle_M16.tscn").Instance<M16>();
        AddChild(m16);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }
}
