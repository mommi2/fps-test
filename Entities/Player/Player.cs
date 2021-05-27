using Godot;
using System;

public class Player : KinematicBody
{
    private float MouseSensitivity = 0.3f;
    private Spatial Head;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        Head = GetNode<Spatial>("Head");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion) 
        {
            RotateY(Godot.Mathf.Deg2Rad(-eventMouseMotion.Relative.x * MouseSensitivity));
        }
    }
}
