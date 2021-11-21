using Godot;
using System;

public abstract class Gun : Spatial, IGun
{
    [Export]
    protected float FireRate = 1.0f;

    [Export]
    protected NodePath ShootParticlesPath;

    [Export]
    protected NodePath AnimationPlayerPath;

    public static class Animations {
        public static readonly String Shooting = "Shooting";
    }

    protected Particles ShootParticles;
    protected AnimationPlayer AnimationPlayer;
    public Boolean IsFiring { get; }
    public Boolean IsReloading { get; }    
    public AmmoManager AmmoManager { get; set; }
    
    public abstract void Shoot();
    public abstract void Reload();
    public abstract bool IsShooting();
}