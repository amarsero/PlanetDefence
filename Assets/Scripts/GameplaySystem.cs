using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySystem : MonoBehaviour
{
    public int HangarStartingHealth = 8;
    [SerializeField] SpriteRenderer hitScreenEffect;
    [SerializeField] SpriteRenderer blankScreenEffect;


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
    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> HitTween => _hitTween ??= CreateHitEffectTween();

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

    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> CreateHitEffectTween()
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

    public TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> CreateBlankEffectTween()
    {
        blankScreenEffect.color = new Color(1,1,1,0);
        return blankScreenEffect
           .DOColor(new Color(1, 1, 1, 1), 3)
           .SetEase(Ease.InCubic)
           .SetAutoKill(true)
           .OnComplete(() => blankScreenEffect.color = new Color(1, 1, 1, 0));
    }

    internal void NukeExploded()
    {
        blankScreenEffect.color = new Color(1, 1, 1, 0);
        TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> tween = null;
        DontDestroyOnLoad(this);
        tween = blankScreenEffect
           .DOColor(new Color(1, 1, 1, 1), 2)
           .SetEase(Ease.InCubic)
           .SetAutoKill(false)
           .OnComplete(() =>
           {
               SceneManager.LoadScene("GameOver");
               tween
               .SetDelay(1)
               .SetAutoKill(true)
               .SetEase(Ease.OutCubic)
               .OnComplete(() => Destroy(gameObject))
               .PlayBackwards();
           });
    }
}
