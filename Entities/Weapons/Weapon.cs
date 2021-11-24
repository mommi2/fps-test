using Godot;
using System;

public abstract class Weapon : Spatial, IWeapon
{
    [Signal]
    public delegate void Equipped();

    public bool IsEquipped { get; set; }

    public abstract void Equip();
    public abstract void Unequip();
}