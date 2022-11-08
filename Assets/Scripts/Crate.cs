using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IActionable2D
{
    public float Speed = 2;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);
    }
    public void DoAction()
    {
        Pickup();
        Destroy(gameObject);
    }

    private void Pickup()
    {
        if (UnityEngine.Random.value > 0.5)
        {
            GiveRandomAmmo();
        }
        else
        {
            GiveLowestAmmo();
        }
    }

    private void GiveLowestAmmo()
    {
        float minPerc = Mathf.Min(
            WeaponSelector.Instance.MissileLauncher.Ammo / 8,
            WeaponSelector.Instance.FragbombLauncher.Ammo / 2,
            WeaponSelector.Instance.Laser.MaxEnergy / WeaponSelector.Instance.Laser.Energy * 1.3f);
        if (WeaponSelector.Instance.Laser.MaxEnergy != WeaponSelector.Instance.Laser.Energy &&
            WeaponSelector.Instance.Laser.MaxEnergy / WeaponSelector.Instance.Laser.Energy * 1.3f == minPerc)
        {
            WeaponSelector.Instance.Laser.GiveAmmo();
        }
        if (minPerc == 2 / WeaponSelector.Instance.FragbombLauncher.Ammo)
        {
            WeaponSelector.Instance.FragbombLauncher.GiveAmmo();
        }
        else if (minPerc == 8 / WeaponSelector.Instance.MissileLauncher.Ammo)
        {
            WeaponSelector.Instance.MissileLauncher.GiveAmmo();
        }
        else if (UnityEngine.Random.value > 0.5)
        {
            WeaponSelector.Instance.FragbombLauncher.GiveAmmo();
        }
        else
        {
            WeaponSelector.Instance.MissileLauncher.GiveAmmo();
        }
    }
    private void GiveRandomAmmo()
    {
        float random = UnityEngine.Random.value;
        if (random < 0.3)
        {
            WeaponSelector.Instance.FragbombLauncher.GiveAmmo();
        }
        else if (random < 0.6)
        {
            WeaponSelector.Instance.MissileLauncher.GiveAmmo();
        }
        else if (WeaponSelector.Instance.Laser.MaxEnergy != WeaponSelector.Instance.Laser.Energy)
        {
            WeaponSelector.Instance.Laser.GiveAmmo();
        }
        else if (UnityEngine.Random.value > 0.5)
        {
            WeaponSelector.Instance.FragbombLauncher.GiveAmmo();
        }
        else
        {
            WeaponSelector.Instance.MissileLauncher.GiveAmmo();
        }
    }
}
