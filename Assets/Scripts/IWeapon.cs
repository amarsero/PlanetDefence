using UnityEngine;

public interface IWeapon
{
    GameObject gameObject { get; }    
    void WeaponShoot(Vector3 worldPosition);
}