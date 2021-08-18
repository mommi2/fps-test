using Godot;
using System;

public class M1911 : EquipableGun
{
    public override void _Ready()
    {
        GD.Print("M1911");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {   
        if (IsEquipped)
        {
            if (Input.IsActionPressed("shoot") && AmmoManager.HasAmmoInMagazine()) Shoot();
            if (Input.IsActionPressed("reload") && !AmmoManager.IsMagazineFull()) Reload();
        }
    }

    public override void Reload()
    {
        AmmoManager.ReloadMagazine();
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunAmmoChanged", AmmoManager);
    }

    public override void Shoot()
    {
        GD.Print("M1911 shoot");
        AmmoManager.Consume();
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunAmmoChanged", AmmoManager);
    }

    public override void Equip()
    {
        GD.Print("M1911 equip");
        IsEquipped = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunEquipped", this);
    }

    public override void Unequip()
    {
        GD.Print("M1911 unequip");
        IsEquipped = false;
    }
}