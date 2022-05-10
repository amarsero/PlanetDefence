using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IWeapon
{
    public GameObject Bullet;
    public Transform BulletOrigin;
    public float ShootingDelay = 0.2f;
    public float ShootingSpeed = 200;
    private float lastShotTime;

    public void WeaponShoot(Vector3 worldPosition)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, worldPosition - transform.position);
        CommandShoot();
    }

    private void CommandShoot()
    {
        if (lastShotTime + ShootingDelay > Time.time)
        {
            return;
        }
        GameObject gameobject = Instantiate(Bullet, BulletOrigin.position, BulletOrigin.rotation);
        gameobject.GetComponent<Bullet>().Speed = ShootingSpeed;
        lastShotTime = Time.time;
    }
}
