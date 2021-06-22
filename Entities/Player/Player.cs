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
    private RayCast GroundCheck;

    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        Head = GetNode<Spatial>("Head");
        GroundCheck = GetNode<RayCast>("GroundCheck");
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

    public Dictionary<string, object> GetDebugData()
    {
        return new Dictionary<string, object>()
        {
            { "Ground Check", GroundCheck.IsColliding().ToString() },
            { "Velocity (y)", Velocity.y }
        };
    }
    
    public override void _PhysicsProcess(float delta)
    {
        Direction = Vector3.Zero;

        if (Input.IsActionPressed("ui_cancel"))
        {
            Input.SetMouseMode(Input.MouseMode.Visible);
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
        if (Velocity.y < -MaxGravity) 
        {
            Velocity.y = -MaxGravity;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            Velocity.y = JumpVelocity;
        }

        int currAccelaration = IsOnFloor() ? Accelaration : AirAccelaration;
        int currSpeed = Input.IsActionPressed("sprint") ? SprintSpeed : Speed;
        //NOTA: Pensa alla interpolazione lineare tra due colori
        Velocity = Velocity.LinearInterpolate(Direction * currSpeed, currAccelaration * delta);

        

        Velocity = MoveAndSlide(Velocity, Vector3.Up);
    }
}
