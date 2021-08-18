using Godot;
using System;

public class AmmoManager : Node, IAmmoManager
{   
    public int AmmoMagazine { get; set; }
    public int MagazineSize { get; set; }
    public int ExtraAmmo { get; set; }

    public AmmoManager(int ammoMagazine, int magazineSize, int extraAmmo)
    {
        AmmoMagazine = ammoMagazine;
        MagazineSize = magazineSize;
        ExtraAmmo = extraAmmo;
    }

    public Boolean IsMagazineFull() => AmmoMagazine == MagazineSize;

    public Boolean HasAmmoInMagazine() => AmmoMagazine > 0;

    public void ReloadMagazine()
    {
        if (IsMagazineFull()) return;
        int ammoNeeded = MagazineSize - AmmoMagazine;
        int ammoToReload = 0;

        if (ammoNeeded > ExtraAmmo)
        {
            ammoToReload = AmmoMagazine + ExtraAmmo;
            ExtraAmmo = 0;
        }
        else
        {
            ammoToReload = MagazineSize;
            ExtraAmmo = ExtraAmmo - ammoNeeded;
        }
        
        AmmoMagazine = ammoToReload;
    }

    public void UpdateAmmo()
    {
        if (AmmoMagazine == 0) return;
        AmmoMagazine = AmmoMagazine - 1;
    }
}