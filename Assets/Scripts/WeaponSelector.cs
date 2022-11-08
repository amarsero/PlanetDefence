using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public Turret Turret;
    public Laser Laser;
    public FragbombLauncher FragbombLauncher;
    public MissileLauncher MissileLauncher;
    public Weapon SelectedWeapon;
    public static WeaponSelector Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (!(Turret && Laser && FragbombLauncher && MissileLauncher))
        {
            throw new System.Exception("A weapon is missing!");
        }
        PlayerController.Instance.OnShoot += PlayerControllerOnShoot;
        if (SelectedWeapon is null)
        {
            SelectedWeapon = Turret;
        }
        SwitchToWeapon(SelectedWeapon);
        Instance = this;
    }

    internal void SwitchToWeapon(Weapon weapon)
    {
        SelectedWeapon = weapon;
        Turret.Deselect();
        Laser.Deselect();
        FragbombLauncher.Deselect();
        MissileLauncher.Deselect();

        SelectedWeapon.Select();
    }

    private void OnDestroy()
    {
        PlayerController.Instance.OnShoot -= PlayerControllerOnShoot;
    }

    private void PlayerControllerOnShoot(Vector3 worldPosition)
    {
        SelectedWeapon?.WeaponShoot(worldPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
