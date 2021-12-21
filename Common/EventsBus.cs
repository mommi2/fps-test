using Godot;
using System;

public class EventsBus : Node
{
    [Signal]
    public delegate void WeaponEquipped(Weapon weapon);

    [Signal]
    public delegate void GunAmmoChanged(AmmoManager ammoManager);

    [Signal]
    public delegate void InteractionLabelVisibility(bool visible, string interactText);

    [Signal]
    public delegate void HudReady();
}