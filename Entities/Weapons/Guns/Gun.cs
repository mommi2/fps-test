using Godot;
using System;

public abstract class Gun : MeshInstance, IGun
{
    [Export]
    protected float FireRate = 1.0f;

    public Boolean IsFiring { get; }
    public Boolean IsReloading { get; }    
    public AmmoManager AmmoManager { get; set; }
    
    public abstract void Shoot();
    public abstract void Reload();
}