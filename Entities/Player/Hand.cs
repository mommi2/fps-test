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

        M16 m16 = ResourceLoader.Load<PackedScene>("res://Entities/Weapons/Guns/Rilfes/M16/M16.tscn").Instance<M16>();
        m16.AmmoManager = new AmmoManager(30, 30, 160);
        m16.Connect("Equipped", this, "test");
        EquipWeapon(m16);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public void test()
    {
        GD.Print("m16 equipped from hand");
    }

    public void EquipWeapon(EquipableGun equipableWeapon)
    {
        Primary.AddChild(equipableWeapon);
        equipableWeapon.Equip();
    }
}
