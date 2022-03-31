using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DORotate(new Vector3(0, 0, -280), 2, RotateMode.FastBeyond360);
        transform.DOScale(scale, 1).SetLoops(2, LoopType.Yoyo).OnComplete(() => Destroy(gameObject));
    }
}
