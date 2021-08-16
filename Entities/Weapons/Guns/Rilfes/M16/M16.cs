using Godot;
using System;

public class M16 : MeshInstance, IGun, IEquipable
{
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Input.IsActionPressed("shoot"))
        {
            Shoot();
        }
        if (Input.IsActionPressed("reload"))
        {
            Reload();
        }
    }

    public void Shoot()
    {
        GD.Print("M16 shoot");
    }

    public void Reload()
    {
        GD.Print("M16 reload");
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
        GD.Print("M16 equip");
    }

    public void Unequip()
    {
        GD.Print("M16 unequip");
    }
}
