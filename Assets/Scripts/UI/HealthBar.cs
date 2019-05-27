using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foreground;
    [SerializeField] private Image midleground;
    [SerializeField] private Image background;
    [SerializeField] private Text hpCountText;

    private Damagable _damagable;

    public void SetTarget(Damagable damagable)
    {
        _damagable = damagable;
        _damagable.DamageEvent += OnDamage;
        if (!_damagable.GetComponent<Player>())
            hpCountText = null;
        OnDamage(damagable);
    }

    private void OnDamage(Damagable damagable)
    {
        midleground.DOFillAmount(damagable.Health / damagable.HealthMax, 0.5f);
        foreground.fillAmount = damagable.Health / damagable.HealthMax;
        if(hpCountText)
            hpCountText.text = damagable.Health.ToString();
    }
}
