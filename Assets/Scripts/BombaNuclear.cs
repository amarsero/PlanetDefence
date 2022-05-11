using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaNuclear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        GameplaySystem.Instance.NukeExploded();
        Destroy(this);
    }
}
