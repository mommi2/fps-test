using Godot;
using System;

public class M1911 : MeshInstance, IGun, IEquipable
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void Shoot()
    {
        GD.Print("M1911 fire");
    }

    public void Reload()
    {
        GD.Print("M1911 reload");
    }

    public Boolean IsFiring() 
    {
        return false;
    }

    public Boolean IsReloading()
    {
        return false;
    }

    public void Equip()
    {
        GD.Print("M1911 equip");
    }

    public void Unequip()
    {
        GD.Print("M1911 unequip");
    }
}