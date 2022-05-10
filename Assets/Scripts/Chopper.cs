using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 4;
    public GameObject Crate;
    void Start()
    {
        if (Random.value > 0.5f)
        {
            Speed *= -1;
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float dropTime = Random.value * 2 + 1;
        DG.Tweening.DOVirtual.DelayedCall(dropTime, DropCrate);
        StartCoroutine(CoroutineHelper.WaitAnd(Bullet.delay, () => Destroy(gameObject)));
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void DropCrate()
    {
        Instantiate(Crate, transform.position, Quaternion.identity);
    }
}
