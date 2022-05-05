using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, IActionable2D, IWeapon
{
    private Missile[] missiles = new Missile[] { };
    // Start is called before the first frame update
    void Start()
    {
        missiles = GetComponentsInChildren<Missile>();
        foreach (var missile in missiles)
        {
            missile.transform.parent = null;
        }
    }

    private void OnEnable()
    {
        foreach (var missile in missiles)
        {
            missile.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var missile in missiles)
        {
            if (missile && missile.State == Missile.MissileState.StandBy)
            {
                missile?.gameObject.SetActive(false);
            }
        }
    }

    public void LaunchMissile()
    {
        foreach (Missile missile in missiles)
        {
            if (missile.ReadyToLaunch())
            {
                missile.LaunchMissile();
                return;
            }
        }
    }
    public void WeaponShoot(Vector3 worldPosition)
    {
        LaunchMissile();
    }

    public void DoAction()
    {
        LaunchMissile();
    }
}
