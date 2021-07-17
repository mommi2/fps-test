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
    private Spatial HandLoc;
    private Spatial Hand;
    private int Sway = 50;
    private Vector3 Snap = Vector3.Zero;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        Head = GetNode<Spatial>("Head");
        HandLoc = Head.GetNode<Spatial>("HandLoc");
        Hand = Head.GetNode<Spatial>("Hand");

        Hand.SetAsToplevel(true);

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
    
    public override void _Process(float delta)
    {
        Hand.GlobalTransform = new Transform(Hand.GlobalTransform.basis, HandLoc.GlobalTransform.origin);
        
        Vector3 handRotation = Hand.Rotation;
        handRotation.x = Mathf.LerpAngle(Hand.Rotation.x, Head.Rotation.x, Sway * delta);
        handRotation.y = Mathf.LerpAngle(Hand.Rotation.y, Rotation.y, Sway * delta);
        Hand.Rotation = handRotation;
    }

    public override void _PhysicsProcess(float delta)
    {
        Direction = Vector3.Zero;

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            Input.SetMouseMode(Input.GetMouseMode() == Input.MouseMode.Visible ? Input.MouseMode.Captured : Input.MouseMode.Visible);
        }

        // =================================== TASTI DIREZIONALI ===================================
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
