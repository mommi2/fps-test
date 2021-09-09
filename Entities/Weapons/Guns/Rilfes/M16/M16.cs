using Godot;
using System;

public class M16 : EquipableGun
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ShootParticles = GetNode<Particles>(ShootParticlesPath);
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
        GD.Print("M16 shoot");
        AmmoManager.Consume();
        ShootParticles.Emitting = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunAmmoChanged", AmmoManager);
    }

    public override void Equip()
    {
        GD.Print("M16 equip");
        IsEquipped = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunEquipped", this);
    }

    public override void Unequip()
    {
        GD.Print("M16 unequip");
        IsEquipped = false;
    }
}
