using Godot;
using System;

public class PlayerHUD : Control
{
    private Label AmmoCounter;

    private Label InteractionLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("HUD");
        AmmoCounter = GetNode<Label>("PanelContainer/AmmoCounter");
        InteractionLabel = GetNode<Label>("InteractionLabel");
        InteractionLabel.Visible = false;

        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("WeaponEquipped", this, "OnWeaponEquipped");
        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("GunAmmoChanged", this, "OnGunAmmoChanged");
        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("InteractionLabelVisibility", this, "OnInteractionLabelVisibility");

        GetNode<EventsBus>(Constants.NodePath.EventsBus).EmitSignal("HudReady");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    private void OnGunAmmoChanged(AmmoManager ammoManager)
    {
        UpdateAmmo(ammoManager);
    }

    public void UpdateAmmo(AmmoManager ammoManager)
    {
        AmmoCounter.Text = $"{ammoManager.AmmoMagazine.ToString().PadLeft(2, '0')}/{ammoManager.ExtraAmmo.ToString().PadLeft(3, '0')}";
    }

    private void OnWeaponEquipped(Weapon weapon)
    {
        GD.Print("Weapon Equipped from HUD");

    	if (weapon is Gun gun)
        {
            UpdateAmmo(gun.AmmoManager);
        }
    }

    private void OnInteractionLabelVisibility(bool visible, string interactText)
    {
        if (!visible)
        {
            InteractionLabel.Visible = false;
            InteractionLabel.Text = "";
            return;
        }

        InteractionLabel.Text = $"Premi E per {interactText ?? "interagire"}";
        InteractionLabel.Visible = true;
    }
}
