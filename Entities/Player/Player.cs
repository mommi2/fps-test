using Godot;
using System;

public class Player : KinematicBody
{
    [Export]
    private float MouseSensitivity = 0.3f;
    
    [Export]
    private int Speed = 10;
    
    [Export]
    private int Accelaration = 15;

    [Export]
    private float Gravity = 40;

    [Export]
    private int JumpVelocity = 15;

    [Export]
    private int AirAccelaration = 0;

    private Vector3 Velocity;
    private Spatial Head;
    private Vector3 Direction;

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
        
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            Velocity.y = JumpVelocity;
        }

        int currAccelaration = IsOnFloor() ? Accelaration : AirAccelaration;
        //NOTA: Pensa alla interpolazione lineare tra due colori
        Velocity = Velocity.LinearInterpolate(Direction * Speed, currAccelaration * delta);
        
        

        Velocity = MoveAndSlide(Velocity, Vector3.Up);
    }
}
