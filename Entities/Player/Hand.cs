using Godot;
using System;

public class Hand : Spatial
{
    private Spatial Primary;
    private Spatial Secondary;
    
    public override void _Ready()
    {
        Primary = GetNode<Spatial>("Primary");
        Secondary = GetNode<Spatial>("Secondary");

        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("HudReady", this, "OnHudReady");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    private void OnHudReady()
    {
        M16 m16 = ResourceLoader.Load<PackedScene>(Constants.Scene.M16).Instance<M16>();
        m16.AmmoManager = new AmmoManager(30, 30, 160);
        EquipWeapon(m16);
    }

    public void EquipWeapon(EquipableGun equipableWeapon)
    {
        Primary.AddChild(equipableWeapon);
        equipableWeapon.Equip();
    }
}
