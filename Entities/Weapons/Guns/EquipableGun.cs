using Godot;
using System;

public abstract class EquipableWeapon : IEquipableWeapon
{
    [Signal]
    public delegate void Equipped();

    public Boolean IsEquipped { get; set; }

    public abstract void Equip();
    public abstract void Unequip();
}