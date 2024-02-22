using UnityEngine;

public interface IShootable 
{
    void Fire();

    void Reload();

    Transform TakeGrabPosition();
}
