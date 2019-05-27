using System;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject damagePrefab;

    private GameObject _healthBarObj;

    public float Health { get; private set; }
    public float HealthMax { get { return maxHealth; } }

    public Action<Damagable> DamageEvent;
    public Action<Damagable> DieEvent;

    private void OnEnable()
    {
        Health = maxHealth;
        if (!_healthBarObj)
        {
            _healthBarObj = Instantiate(GameManager.Instance.HealthBar.gameObject);
            _healthBarObj.transform.SetParent(GameManager.Instance.MainCanvas.transform);
            HealthBar _healthBar = _healthBarObj.GetComponent<HealthBar>();
            _healthBar.SetTarget(this);
            TargetWatchUI _target = _healthBarObj.GetComponent<TargetWatchUI>();
            if (_target)
                _target.target = transform;
        }
        _healthBarObj.SetActive(true);
        DamageEvent?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageProvider = other.GetComponentInParent<DamageProvider>();
        if (damageProvider != null)
        {
            Health -= damageProvider.Damage;
            DamageEvent?.Invoke(this);
            GameManager.Instance.Pools.Spawn(damagePrefab, transform.position, transform.rotation);

            if (Health <= 0)
            {
                DieEvent?.Invoke(this);
                _healthBarObj.SetActive(false);
                gameObject.DestroyOrMoveToPool();
            }
            if (damageProvider.DestroyAfterCollide)
                damageProvider.gameObject.DestroyOrMoveToPool();

            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon)
            {
                weapon.AttackCollider.enabled = false;
            }
        }
    }
}

