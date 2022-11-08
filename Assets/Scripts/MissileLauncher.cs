using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileLauncher : Weapon, IActionable2D
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

    internal void GiveAmmo()
    {
        Ammo += 4;
        int count = missiles.Count(x => x.enabled);
        foreach (var missile in missiles.Concat(missiles).Skip(lastShotIndex))
        {
            if (Ammo >= count)
            {
                return;
            }
            if (missile.enabled)
            {
                continue;
            }
            missile.enabled = true;
            count += 1;
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

    override public void WeaponShoot(Vector3 worldPosition)
    {
        CommandShoot();
    }

    public void DoAction()
    {
        CommandShoot();
    }

    override public bool CanShoot()
    {
        return Ammo > 0;
    }
}
