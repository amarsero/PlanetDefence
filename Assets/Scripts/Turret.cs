using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private bool _shooting;

    public GameObject Bullet;
    public Transform BulletOrigin;
    public float ShootingDelay = 0.2f;
    public float ShootingSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.OnShoot += PlayerControllerOnShoot;
    }
    private void OnDestroy()
    {
        PlayerController.Instance.OnShoot -= PlayerControllerOnShoot;
    }

    private void PlayerControllerOnShoot(Vector3 worldPosition)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, worldPosition - transform.position);
        CommandShoot();
    }

    private void CommandShoot()
    {
        if (_shooting)
        {
            return;
        }
        _shooting = true;
        GameObject gameobject = Instantiate(Bullet, BulletOrigin.position, BulletOrigin.rotation);
        gameobject.GetComponent<Bullet>().Speed = ShootingSpeed;
        StartCoroutine(CoroutineHelper.WaitSecondsAnd(ShootingDelay, () => { _shooting = false; }));
    }
}
