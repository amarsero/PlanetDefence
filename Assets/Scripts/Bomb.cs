using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IDamageable
{
    public GameObject Explosion;
    public float Lifetime = 10;
    [SerializeField]
    public float Speed = 200;

    public float Health = 1;
    public static WaitForSeconds delay;

    void Awake()
    {
        delay ??= new WaitForSeconds(Lifetime);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -Speed));
        StartCoroutine(CoroutineHelper.WaitAnd(delay, () => Destroy(gameObject)));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            return;
        }
        Explode();
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void DoDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Explode();
        }
    }
}
