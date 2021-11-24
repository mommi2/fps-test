using Godot;
using System;

public abstract class Gun : Weapon, IGun
{
    [Export]
    protected NodePath ShootParticlesPath;

    [Export]
    protected NodePath AnimationPlayerPath;

    public static class Animations 
    {
        public static readonly String Shooting = "Shooting";
    }

    protected Particles ShootParticles;
    protected AnimationPlayer AnimationPlayer;   
    public AmmoManager AmmoManager { get; set; }
    
    public abstract void Shoot();
    public abstract void Reload();
    public abstract bool IsShooting();
}