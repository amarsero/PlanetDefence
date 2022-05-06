using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    public int HangarStartingHealth = 8;
    [SerializeField] SpriteRenderer hitScreenEffect;
    static GameplaySystem _instance;
    public static GameplaySystem Instance => _instance ?? throw new System.NullReferenceException("GameplaySystem is Missing!");
    Tween hitScreenTween;
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

    public void Start()
    {
        hitScreenTween = hitScreenEffect
            .DOColor(new Color(1, 1, 1, 1), 0.3f)
            .SetEase(Ease.InCubic)
            .SetAutoKill(false);
        hitScreenTween.Rewind();
    }

    public void PlayerTookHit()
    {
        hitScreenTween.fullPosition = 0.3f;
        hitScreenTween.PlayBackwards();
    }
}
