using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Explosion == null)
            return;
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
