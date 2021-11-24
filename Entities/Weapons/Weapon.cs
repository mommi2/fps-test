using Godot;
using System;

public abstract class Weapon : Spatial, IWeapon
{
    [Signal]
    public delegate void Equipped();


    public static class Animations 
    {
        public static class Gun
        {
            public static readonly String Shooting = "Shooting";
        }
    }


    public bool IsEquipped { get; set; }

    public abstract void Equip();
    public abstract void Unequip();
}