using Godot;
using System;

public class Hand : Spatial
{
    [Export]
    public int MaxSlots;
    private EquipableGun[] GunCarried;
    private int CurrentSlot = 0;
    private Spatial[] Slots;
    
    public override void _Ready()
    {
        GunCarried = new EquipableGun[MaxSlots];
        Slots = new Spatial[MaxSlots];
        GetNode<EventsBus>(Constants.NodePath.EventsBus).Connect("HudReady", this, "OnHudReady");
        for (int i = 0; i < MaxSlots; i++)
        {
            Slots[i] = GetNode<Spatial>($"Slot_{i}");
        }
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
        if (CurrentSlot + 1 >= MaxSlots) return;
        GunCarried[CurrentSlot].Unequip();
        Slots[CurrentSlot].Visible = false;
        CurrentSlot++;
        Slots[CurrentSlot].Visible = true;
        GunCarried[CurrentSlot].Equip();
    }

    public void PreviousWeapon()
    {
        if (CurrentSlot - 1 < 0) return;
        GunCarried[CurrentSlot].Unequip();
        Slots[CurrentSlot].Visible = false;
        CurrentSlot--;
        Slots[CurrentSlot].Visible = true;
        GunCarried[CurrentSlot].Equip();
    }

    private void OnHudReady()
    {
        M16 m16 = ResourceLoader.Load<PackedScene>(Constants.Scene.M16).Instance<M16>();
        M1911 m1911 = ResourceLoader.Load<PackedScene>(Constants.Scene.M1911).Instance<M1911>();
        m1911.AmmoManager = new AmmoManager(ammoMagazine: 10, magazineSize: 10, extraAmmo: 60);
        m16.AmmoManager = new AmmoManager(ammoMagazine: 30, magazineSize: 30, extraAmmo: 160);
        PutAllWeapons(new EquipableGun[] {m16, m1911});
    }

    public void PutAllWeapons(EquipableGun[] weapons)
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            GunCarried[i] = weapons[i];
            Slots[i].AddChild(weapons[i]);
            
        }
        weapons[0].Equip();
    }

    public void EquipWeapon(int slotIndex, EquipableGun weapon)
    {
        GunCarried[slotIndex] = weapon;
        Slots[slotIndex].AddChild(weapon);
        weapon.Equip();
    }
}
