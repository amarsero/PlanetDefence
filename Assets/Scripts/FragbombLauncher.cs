using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragbombLauncher : Weapon
{
    public float Ammo = 2;
    public float ShootingDelay = 0.5f;
    public GameObject Fragbomb;
    private GameObject currentBomb;

    private void OnEnable()
    {
        currentBomb?.SetActive(true); 
        if (currentBomb == null && Ammo > 0)
        {
            CreateBomb();
        }
    }

    private void OnDisable()
    {
        currentBomb?.SetActive(false);
    }

    public void CommandShoot(Vector3 targetPosition)
    {
        if (currentBomb == null)
        {
            return;
        }
        GameObject bomb = currentBomb;
        currentBomb = null;
        var frag = bomb.GetComponent<FragBomb>();
        frag.Target = targetPosition;
        frag.enabled = true;
        Ammo -= 1;
        if (Ammo > 0)
        {
            DG.Tweening.DOVirtual.DelayedCall(ShootingDelay, () => CreateBomb());
        }
    }

    private void CreateBomb()
    {
        currentBomb = null;
        currentBomb = Instantiate(Fragbomb, transform.position + Vector3.forward, transform.rotation);
        currentBomb.GetComponent<FragBomb>().enabled = false;
        currentBomb.SetActive(this.isActiveAndEnabled);
    }

    override public void WeaponShoot(Vector3 worldPosition)
    {
        CommandShoot(worldPosition);
    }

    public override bool CanShoot()
    {
        return Ammo > 0;
    }
}
