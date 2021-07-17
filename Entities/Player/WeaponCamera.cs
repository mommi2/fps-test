using Godot;
using System;

public class WeaponCamera : Camera
{
    
    [Export]
    private NodePath MainCameraPath;

    private Camera MainCamera;

    public override void _Ready()
    {
        MainCamera = GetNode<Camera>(MainCameraPath);
    }

    public override void _Process(float delta)
    {
        GlobalTransform = MainCamera.GlobalTransform;
    }
}
