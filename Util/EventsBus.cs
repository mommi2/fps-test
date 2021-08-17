using Godot;
using System;

public class EventsBus : Node
{
    [Signal]
    public delegate void GunEquipped(EquipableGun equipableGun);
}