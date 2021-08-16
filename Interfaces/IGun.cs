using System;

public interface IGun
{
    void Shoot();
    void Reload();
    Boolean IsReloading();
    Boolean IsFiring();
}
