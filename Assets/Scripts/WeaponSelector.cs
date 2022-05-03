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
    public IWeapon SelectedWeapon;
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
    }

    internal void SwitchToWeapon(IWeapon weapon)
    {
        SelectedWeapon = weapon;
        if (!object.ReferenceEquals(Turret,SelectedWeapon)) Turret.gameObject.SetActive(false);
        if (!object.ReferenceEquals(Laser, SelectedWeapon)) Laser.gameObject.SetActive(false);
        if (!object.ReferenceEquals(FragbombLauncher, SelectedWeapon)) FragbombLauncher.gameObject.SetActive(false);
        if (!object.ReferenceEquals(MissileLauncher, SelectedWeapon)) MissileLauncher.gameObject.SetActive(false);
        SelectedWeapon.gameObject.SetActive(true);
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
