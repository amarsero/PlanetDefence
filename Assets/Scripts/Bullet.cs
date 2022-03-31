using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Lifetime = 10;
    [SerializeField]
    public float Speed = 200;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.rotation * new Vector2(0, Speed));
        StartCoroutine(CoroutineHelper.WaitSecondsAnd(Lifetime, () => Destroy(gameObject)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
