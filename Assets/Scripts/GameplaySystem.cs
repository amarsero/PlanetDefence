using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    public int HangarStartingHealth = 8;
    [SerializeField] SpriteRenderer hitScreenEffect;
    static GameplaySystem _instance;
    public static GameplaySystem Instance => _instance ?? throw new System.NullReferenceException("GameplaySystem is Missing!");
    [Header("Hit animation")]
    public int Repetitions = 1;
    public float AnimationTime = 0.3f;
    public DG.Tweening.Ease Ease = DG.Tweening.Ease.Linear;
    public Color FromColor = Color.red;
    public Color ToColor = Color.white;
    [Tooltip("Va y vuelve en la animación, en vez de solo ir y volver al principio")]
    public bool Yoyo = false;
    TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> _hitTween;
    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> HitTween;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayerTookHit()
    {
        HitTween.Restart();
    }

    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> CreateTween()
    {
        Color originalColor = hitScreenEffect.color;
        hitScreenEffect.color = FromColor;
        return hitScreenEffect
           .DOColor(ToColor, AnimationTime)
           .SetEase(Ease)
           .SetAutoKill(false)
           .SetLoops(Repetitions, Yoyo ? LoopType.Yoyo : LoopType.Restart)
           .OnComplete(() => hitScreenEffect.color = originalColor);
    }
}
