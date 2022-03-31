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
        for (int i = 0; i < 24; i++)
        {
            GameObject small = Instantiate(Fragment, transform.position + rotation * Vector3.up, rotation);
            small.GetComponent<Bullet>().Lifetime = 3;
            rotation *= Quaternion.Euler(0, 0, 360 / 24);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
