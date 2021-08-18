using System;

public interface IAmmoManager
{
    Boolean IsMagazineFull();

    Boolean HasAmmoInMagazine();

    void ReloadMagazine();

    void Consume();
}