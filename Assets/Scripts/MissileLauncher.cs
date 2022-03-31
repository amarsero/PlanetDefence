using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchMissile();
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
}
