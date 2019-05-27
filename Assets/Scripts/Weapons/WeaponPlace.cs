using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlace : MonoBehaviour {

    [SerializeField] private Transform weaponPlaceTransform;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange;
    [SerializeField] private bool isPlayer;

    public Weapon CurrentWeapon { get; private set; }

    private GameObject _target;
    private GameObject _targetCircle;

    private void OnEnable()
    {
         CreateWeapon(weapon);
    }

    private void CreateWeapon(Weapon weaponPrefab)
    {
        if (!CurrentWeapon)
        {
            CurrentWeapon = Instantiate(weaponPrefab, weaponPlaceTransform.position, weapon.transform.rotation);
            CurrentWeapon.transform.SetParent(weaponPlaceTransform); // по другому привязать не получается
        }
        CurrentWeapon.SetAnimation(animator);
    }

    private void Update()
    {
        if (isPlayer)
        {
            if (_target && _target.activeSelf && Player.Instance.CanShoot)
            {
                transform.LookAt(_target.transform);
                this.Attack(animator);
            }
            else
            {
                _target = FindNearestEnemy();
                if (_target)
                {
                    if (!_targetCircle)
                        _targetCircle = Instantiate(GameManager.Instance.TargetCircle);
                    _targetCircle.transform.SetParent(_target.transform);
                    _targetCircle.transform.position = new Vector3(_target.transform.position.x, 1f, _target.transform.position.z);
                }
            }
        }
        else
        {
            if(Player.Instance  )
                EnemyHit();
        }
    }

    public void AttackStartEvent()
    {
        if(CurrentWeapon.AttackCollider)
            CurrentWeapon.AttackCollider.enabled = true;
        if (CurrentWeapon.Gun && _target && Player.Instance.CanShoot)
            CurrentWeapon.Shoot(_target.transform);
        else if (CurrentWeapon.Gun && !isPlayer)
            CurrentWeapon.Shoot(Player.Instance.transform);
    }

    public void AttackStopEvent()
    {
        if (CurrentWeapon.AttackCollider)
            CurrentWeapon.AttackCollider.enabled = false;
    }
    private GameObject FindNearestEnemy()
    {
        float minDistance = attackRange;
        GameObject nearestEnemy = null;

        foreach (var enemy in GameManager.Instance.Enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }


    private void EnemyHit()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) < attackRange)
        {
            transform.LookAt(Player.Instance.transform.position);
            this.Attack(animator);
        }
    }
}

