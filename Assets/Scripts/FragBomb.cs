using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragBomb : MonoBehaviour
{
    public GameObject Fragment;
    public GameObject Explosion;
    public float Speed = 200;
    public float Lifetime = 3;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.rotation * new Vector2(0, Speed));
        StartCoroutine(CoroutineHelper.WaitSecondsAnd(Lifetime, () => Explode()));
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position + Vector3.back, transform.rotation);
        Quaternion rotation = Quaternion.identity;
        Quaternion stepRot = Quaternion.Euler(0, 0, 9 /*360 / 40*/);
        Vector3 smallUp = new Vector3(0, 0.5f);
        for (int i = 0; i < 40; i++)
        {
            Bullet small = Instantiate(Fragment, transform.position + rotation * (i % 2 == 1 ? smallUp : Vector3.up), rotation).GetComponent<Bullet>();
            small.Lifetime = 3;
            small.Speed = i % 2 == 1 ? 230 : 200;
            rotation *= stepRot;
        }
        Destroy(gameObject);
    }

}
