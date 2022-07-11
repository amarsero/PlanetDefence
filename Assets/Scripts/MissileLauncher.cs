using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, IActionable2D, IWeapon
{
    public int Ammo = 6;
    public float ShootingDelay = 0.2f;
    private float lastShotTime = 0;
    private int lastShotIndex = 0;
    private Missile[] missiles = new Missile[] { };

    void Start()
    {
        missiles = GetComponentsInChildren<Missile>();
    }

    public void LaunchMissile()
    {
        Missile missile = missiles[lastShotIndex++];
        lastShotIndex %= missiles.Length;
        lastShotTime = Time.time;
        GameObject newMissile = Instantiate(missile.gameObject, missile.transform.position, missile.transform.rotation, transform);
        newMissile.transform.parent = null;
        newMissile.SetActive(true);
        newMissile.GetComponent<Missile>().LaunchMissile();
        Ammo -= 1;
        if (Ammo < missiles.Length)
        {
            missile.gameObject.SetActive(false);
        }
    }

    private void CommandShoot()
    {
        if (Ammo <= 0)
        {
            return;
        }
        if (lastShotTime + ShootingDelay > Time.time)
        {
            return;
        }
        LaunchMissile();
    }

    public void WeaponShoot(Vector3 worldPosition)
    {
        CommandShoot();
    }

    public void DoAction()
    {
        CommandShoot();
    }
}
