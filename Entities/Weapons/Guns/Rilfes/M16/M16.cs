using Godot;
using System;

public class M16 : Gun
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ShootParticles = GetNode<Particles>(ShootParticlesPath);
        AnimationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {   
        if (IsEquipped)
        {
            if (Input.IsActionPressed("shoot") && AmmoManager.HasAmmoInMagazine() && !IsShooting()) Shoot();
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
        AnimationPlayer.Play(Animations.Gun.Shooting);
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("GunAmmoChanged", AmmoManager);
    }

    public override void Equip()
    {
        GD.Print("M16 equip");
        IsEquipped = true;
        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("WeaponEquipped", this);
    }

    public override void Unequip()
    {
        GD.Print("M16 unequip");
        IsEquipped = false;
    }

    public override bool IsShooting()
    {
        return AnimationPlayer.IsPlaying() && AnimationPlayer.CurrentAnimation == Animations.Gun.Shooting;
    }
}
