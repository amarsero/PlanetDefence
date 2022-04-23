using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    public bool SPINNN = false;
    public bool chico_GRANDE_chico = false;
    [Tooltip("Tiempo que está viva la exploción hasta que se destruye. Duración de los efectos.")]
    public float timeToLive = 2;
    // Update is called once per frame
    void Start()
    {
        if (chico_GRANDE_chico)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = Vector3.zero;
            transform.DOScale(scale, timeToLive / 2).SetLoops(2, LoopType.Yoyo);
        }
        if (SPINNN)
        {
            transform.DORotate(new Vector3(0, 0, -280), timeToLive, RotateMode.FastBeyond360);
        }
        DOVirtual.DelayedCall(timeToLive, () => Destroy(gameObject));
    }
}
