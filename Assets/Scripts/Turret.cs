using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private bool _shooting;

    public float RotationSpeed = 50;
    public GameObject Bullet;
    public Transform BulletOrigin;
    public float ShootingDelay = 0.2f;
    public float ShootingSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector3 pos = Input.GetMouseButton(0) ? Input.mousePosition : new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.z = 0;
            MoveAndShootWithTouch(pos);
        }
        else
        {
            int rotation = 0;

            if (Input.GetKey(KeyCode.A))
            {
                rotation++;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotation--;
            }
            transform.Rotate(Vector3.forward, rotation * Time.deltaTime * RotationSpeed);

            if (Input.GetKey(KeyCode.Space))
            {
                CommandShoot();
            }
        }
    }

    private void MoveAndShootWithTouch(Vector3 vector3)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up  , vector3 - transform.position);
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
