using Godot;
using System;

public class Player : KinematicBody
{
    private float MouseSensitivity = 0.3f;
    private Spatial Head;
    private Vector3 Direction;
    private int Speed = 50;
    private int Accelaration = 6;
    private Vector3 Velocity;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        Head = GetNode<Spatial>("Head");
        GD.Print("Ready");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion) 
        {
            RotateY(Godot.Mathf.Deg2Rad(-eventMouseMotion.Relative.x * MouseSensitivity));
            Head.RotateX(Godot.Mathf.Deg2Rad(-eventMouseMotion.Relative.y * MouseSensitivity));

            Vector3 headRotation = Head.Rotation;
            headRotation.x = Godot.Mathf.Clamp(Head.Rotation.x, Godot.Mathf.Deg2Rad(-89), Godot.Mathf.Deg2Rad(89));
            Head.Rotation = headRotation;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        Direction = Vector3.Zero;

        if (Input.IsActionPressed("ui_cancel"))
        {
            Input.SetMouseMode(Input.MouseMode.Visible);
        }

        // TASTI DIREZIONALI ===================================
        if (Input.IsActionPressed("move_forward"))
        {
            Direction -= Transform.basis.z;
        }
        if (Input.IsActionPressed("move_backward"))
        {
            Direction += Transform.basis.z;
        }
        if (Input.IsActionPressed("move_left"))
        {
            Direction -= Transform.basis.x;
        }
        if (Input.IsActionPressed("move_right"))
        {
            Direction += Transform.basis.x;
        }

        Direction = Direction.Normalized();

        //Pensa alla interpolazione lineare tra due colori
        Velocity = Velocity.LinearInterpolate(Direction * Speed, Accelaration * delta);
        MoveAndSlide(Velocity, Vector3.Up);
    }
}
