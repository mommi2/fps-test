using Godot;
using System;

public class AmmoBox: StaticBody, IInteractable
{
    public void Interact(Player player)
    {
        GD.Print("Rifornimento ammo");
    }

    public string GetInteractText() => "rifornirti";
}