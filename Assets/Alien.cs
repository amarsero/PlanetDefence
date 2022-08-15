using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, IDamageable
{
    public float Health = 3;
    public GameObject Explosion;

    public void DoDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position, transform.rotation).transform.localScale = Vector3.one * 2;
        Destroy(gameObject);
    }
}
