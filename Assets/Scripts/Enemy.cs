using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private WeaponPlace weaponPlace;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Damagable _damagable;
    private float attackTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        GameManager.Instance.Enemies.Add(gameObject);
    }
    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if (Player.Instance)
            _navMeshAgent.SetDestination(Player.Instance.transform.position);

        if (Mathf.Approximately(_navMeshAgent.velocity.magnitude, 0f))
        {
            CharacterAnimController.Walk(_animator, false);
        }
        else
        {
            CharacterAnimController.Walk(_animator, true);
            float forwardSpeed = _navMeshAgent.velocity.magnitude;
            CharacterAnimController.SpeedWalk(_animator, forwardSpeed);
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.Enemies.Remove(gameObject);
    }
}
