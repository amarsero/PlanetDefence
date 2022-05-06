using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    public int Health = 8;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Health.ToString();
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)Health / GameplaySystem.Instance.HangarStartingHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)Health / GameplaySystem.Instance.HangarStartingHealth);
        GameplaySystem.Instance.PlayerTookHit();
        text.text = (--Health).ToString();
        if (Health == 0)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
