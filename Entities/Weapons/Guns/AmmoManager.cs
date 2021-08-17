using Godot;
using System;

public class AmmoManager : IAmmoManager
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
}