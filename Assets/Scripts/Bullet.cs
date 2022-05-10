using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Lifetime = 10;
    [SerializeField]
    public float Speed = 200;
    public static WaitForSeconds delay;

    void Awake()
    {
        delay ??= new WaitForSeconds(Lifetime);
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.rotation * new Vector2(0, Speed));
        StartCoroutine(CoroutineHelper.WaitAnd(delay, () => OnLifeEnd()));
    }

    private void OnLifeEnd()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        DOTween.Sequence(this)
            .Append(DOTween.To(() => rigid.velocity, x => rigid.velocity = x, Vector2.zero, 2))
            .Insert(0, transform.DOScale(Vector3.zero, 2))
            .AppendInterval(0.1f)
            .AppendCallback(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        var damagable = collision.GetComponent<IDamageable>();
        if (damagable != null)
        {
            damagable.DoDamage(1);
        }
        Destroy(gameObject);
    }
}
