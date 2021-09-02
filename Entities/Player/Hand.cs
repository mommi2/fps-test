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
        PutAllWeapons(new EquipableGun[] {m16, m1911});
    }

    public void PutAllWeapons(EquipableGun[] weapons)
    {
        int maxWeapons = MaxSlots >= weapons.Length? weapons.Length : MaxSlots;

        if (maxWeapons == 0) return;
        
        for (int i = 0; i < maxWeapons; i++)
        {
            GunCarried[i] = weapons[i];
            Slots[i].AddChild(weapons[i]);
            
        }
        GunCarried[0].Equip();
    }

    public void UnequipWeapon(int slotIndex)
    {
        GunCarried[slotIndex].Unequip();
        Slots[slotIndex].Visible = false;
    }

    public void EquipWeapon(int slotIndex)
    {
        Slots[slotIndex].Visible = true;
        GunCarried[slotIndex].Equip();
    }
}
