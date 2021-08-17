using Godot;
using System;

public class M16 : EquipableGun
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {   
        if (IsEquipped)
        {
            if (Input.IsActionPressed("shoot")) Shoot();
            if (Input.IsActionPressed("reload")) Reload();
            
        }
    }

    public override void Reload()
    {
        GD.Print("M16 reload");
    }

    public override void Shoot()
    {
        GD.Print("M16 shoot");
    }

    public override void Equip()
    {
        GD.Print("M16 equip");
        IsEquipped = true;
        GetNode<EventsBus>(Constants.EventsBusPath).EmitSignal("GunEquipped", this);
    }

    public override void Unequip()
    {
        GD.Print("M16 unequip");
        IsEquipped = false;
    }
}
