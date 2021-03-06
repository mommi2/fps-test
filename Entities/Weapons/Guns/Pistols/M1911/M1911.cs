using Godot;
using System;

public class M1911 : Gun
{
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
        GD.Print("M1911 shoot");
        AmmoManager.Consume();
        ShootParticles.Emitting = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunAmmoChanged", AmmoManager);
    }

    public override void Equip()
    {
        GD.Print("M1911 equip");
        IsEquipped = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("WeaponEquipped", this);
    }

    public override void Unequip()
    {
        GD.Print("M1911 unequip");
        IsEquipped = false;
    }

    public override bool IsShooting()
    {
        throw new NotImplementedException();
    }
}