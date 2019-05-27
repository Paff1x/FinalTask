using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private WeaponPlace weaponPlace;
    private Animator _animator;

    private Vector3 _moveVector;
    private CharacterController _chController;
    private Transform _target;

    public static Player Instance { get; private set; }
    public bool CanShoot { get; set; }
    private void Awake()
    {
        Instance = this;
        _chController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = Joystick.Horizontal;
        _moveVector.z = Joystick.Vertical;


        if (_moveVector.x != 0 || _moveVector.z != 0)
        {
            CanShoot = false;
            CharacterAnimController.Walk(_animator, true);
            CharacterAnimController.SpeedWalk(_animator, Mathf.Abs(_moveVector.x) + Mathf.Abs(_moveVector.z));
        }
        else
        {
            CanShoot = true;
            CharacterAnimController.Walk(_animator, false);
        }

        float angle = Vector3.Angle(Vector3.forward, _moveVector);
        if (angle > 1f || angle == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        _chController.Move(_moveVector * moveSpeed * Time.deltaTime);
    }
}
