using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    public int Health = 8;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Explosion;
    [SerializeField] GameObject BlownUp;
    [Header("Hit animation")]
    public int Repetitions = 1;
    public float AnimationTime = 0.3f;
    public DG.Tweening.Ease Ease = DG.Tweening.Ease.Linear;
    public Color FromColor = Color.red;
    public Color ToColor = Color.white;
    [Tooltip("Va y vuelve en la animación, en vez de solo ir y volver al principio")]
    public bool Yoyo = false;
    DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> _healthTween;
    public DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> HealthTween
    {
        get
        {
            if (_healthTween == null)
            {
                _healthTween = CreateTween();
            }
            return _healthTween;
        }
    }
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
        GameplaySystem.Instance.PlayerTookHit();
        text.text = (--Health).ToString();
        if (Health == 0)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Instantiate(BlownUp, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            HealthTween.Restart();
        }
    }

    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> CreateTween()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = FromColor;
        return spriteRenderer
           .DOColor(ToColor, AnimationTime)
           .SetEase(Ease)
           .SetAutoKill(false)
           .SetLoops(Repetitions, Yoyo ? LoopType.Yoyo : LoopType.Restart)
           .OnComplete(() => spriteRenderer.color = originalColor);
    }
}

