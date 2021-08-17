using Godot;
using System;

public abstract class Gun : MeshInstance, IGun
{
    public Boolean IsFiring { get; }
    public Boolean IsReloading { get; }    
    public AmmoManager AmmoManager { get; set; }
    
    public abstract void Shoot();
    public abstract void Reload();
}