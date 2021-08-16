using System;

public interface IGun
{
    void Fire();
    void Reload();
    Boolean IsReloading();
    Boolean IsFiring();
}
