using Godot;
using System;
using System.Collections.Generic;

public class Hand : Spatial
{
    [Export]
    public int MaxSlots;
    private int CurrentSlot = 0;
    private List<Spatial> Slots;
    
    public override void _Ready()
    {
        Slots = new List<Spatial>();
        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("HudReady", this, "OnHudReady");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.Pressed)
            {
                switch (eventMouseButton.ButtonIndex)
                {
                    case ((int)ButtonList.WheelUp):
                        NextWeapon();
                        break;
                    case ((int)ButtonList.WheelDown):
                        PreviousWeapon();
                        break;
                }
            }
        }
    }

    public void NextWeapon()
    {
        if (CurrentSlot + 1 >= Slots.Count) return;
        UnequipWeapon(CurrentSlot);
        CurrentSlot++;
        EquipWeapon(CurrentSlot);
    }

    public void PreviousWeapon()
    {
        if (CurrentSlot - 1 < 0) return;
        UnequipWeapon(CurrentSlot);
        CurrentSlot--;
        EquipWeapon(CurrentSlot);
    }

    private void OnHudReady()
    {
        M16 m16 = ResourceLoader.Load<PackedScene>(Constants.Scene.M16).Instance<M16>();
        M1911 m1911 = ResourceLoader.Load<PackedScene>(Constants.Scene.M1911).Instance<M1911>();
        m1911.AmmoManager = new AmmoManager(ammoMagazine: 10, magazineSize: 10, extraAmmo: 60);
        m16.AmmoManager = new AmmoManager(ammoMagazine: 30, magazineSize: 30, extraAmmo: 160);
        PutAllWeapons(new Weapon[] {m16, m1911});
    }

    public void PutAllWeapons(Weapon[] weapons)
    {
        int maxWeapons = MaxSlots >= weapons.Length? weapons.Length : MaxSlots;

        if (maxWeapons == 0) return;
        
        for (int i = 0; i < maxWeapons; i++)
        {
            PutWeapon(weapons[i]);
        }
        Slots[0].GetChild<Weapon>(0).Equip();
    }

    public void PutWeapon(Weapon weapon)
    {
        if (Slots.Count >= MaxSlots) 
        {
            //TODO: Emette evento 
            return;
        }

        Spatial newSlot = new Spatial();
        newSlot.Name = $"Slot_{Slots.Count}";
        newSlot.AddChild(weapon);
        
        AddChild(newSlot);
        Slots.Add(newSlot);
    }

    public void UnequipWeapon(int slotIndex)
    {
        if (Slots[slotIndex].GetChildCount() == 0) return;
        Slots[slotIndex].GetChild<Weapon>(0).Unequip();
        Slots[slotIndex].Visible = false;
    }

    public void EquipWeapon(int slotIndex)
    {
        if (Slots[slotIndex].GetChildCount() == 0) return;
        Slots[slotIndex].Visible = true;
        Slots[slotIndex].GetChild<Weapon>(0).Equip();
    }
}
