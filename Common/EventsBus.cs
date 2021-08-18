using Godot;
using System;

public class EventsBus : Node
{
    [Signal]
    public delegate void GunEquipped(EquipableGun equipableGun);

    [Signal]
    public delegate void GunAmmoChanged(AmmoManager ammoManager);

    [Signal]
    public delegate void HudReady();
}