using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour, IActionable2D
{
    private Missile[] missiles;
    // Start is called before the first frame update
    void Start()
    {
        missiles = GetComponentsInChildren<Missile>();
        foreach (var missile in missiles)
        {
            missile.transform.parent = null;
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

    public void DoAction()
    {
        LaunchMissile();
    }
}
