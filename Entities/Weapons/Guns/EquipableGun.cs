using Godot;
using System;

public abstract class EquipableGun : Gun, IEquipable
{
    [Signal]
    public delegate void Equipped();

    public Boolean IsEquipped { get; set; }

    public abstract void Equip();
    public abstract void Unequip();
}