using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody, IDebuggable
{
    [Export]
    private float MouseSensitivity = 0.3f;
    
    [Export]
    private int Speed = 10;

    [Export]
    private int SprintSpeed = 20;
    
    [Export]
    private int Accelaration = 15;

    [Export]
    private float Gravity = 40;

    [Export]
    private float MaxGravity = 150;

    [Export]
    private int JumpVelocity = 15;

    [Export]
    private int AirAccelaration = 0;

    private Vector3 Velocity;
    private Vector3 Direction;
    private Spatial Head;
    private Vector3 Snap = Vector3.Zero;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        Head = GetNode<Spatial>("Head");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion) 
        {
            RotateY(Mathf.Deg2Rad(-eventMouseMotion.Relative.x * MouseSensitivity));
            Head.RotateX(Mathf.Deg2Rad(-eventMouseMotion.Relative.y * MouseSensitivity));

            Vector3 headRotation = Head.Rotation;
            headRotation.x = Mathf.Clamp(Head.Rotation.x, Mathf.Deg2Rad(-89), Mathf.Deg2Rad(89));
            Head.Rotation = headRotation;
        }
    }

    public Dictionary<string, object> GetDebugData()
    {
        return new Dictionary<string, object>()
        {
            { "Velocity", Velocity },
            { "Direction", Direction }
        };
    }

    private Vector3 GetInputDirection()
    {
        Vector3 direction = Vector3.Zero;

        if (Input.IsActionPressed("move_forward"))
        {
            direction -= Transform.basis.z;
        }
        if (Input.IsActionPressed("move_backward"))
        {
            direction += Transform.basis.z;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction -= Transform.basis.x;
        }
        if (Input.IsActionPressed("move_right"))
        {
            direction += Transform.basis.x;
        }

        return direction.Normalized();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            Input.SetMouseMode(Input.GetMouseMode() == Input.MouseMode.Visible ? Input.MouseMode.Captured : Input.MouseMode.Visible);
        }

        Direction = GetInputDirection();
        
        Velocity.y -= Gravity * delta;
        Velocity.y = (Velocity.y < -MaxGravity) ? Velocity.y = -MaxGravity : Velocity.y;

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            Velocity.y = JumpVelocity;
            Snap = Vector3.Zero;
        }
        else
        {
            Snap = Vector3.Down;
        }

        int currAccelaration = IsOnFloor() ? Accelaration : AirAccelaration;
        int currSpeed = Input.IsActionPressed("sprint") ? SprintSpeed : Speed;
        
        //NOTA: Pensa all'interpolazione lineare tra due colori
        Velocity = Velocity.LinearInterpolate(Direction * currSpeed, currAccelaration * delta);

        MoveAndSlideWithSnap(Velocity, Snap, Vector3.Up, true);
    }
}
