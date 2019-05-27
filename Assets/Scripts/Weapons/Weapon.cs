using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


    [SerializeField] private Collider attackCollider;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject shootParticlePrefab;
    [SerializeField] private AudioClip audio;

    public WeaponType WeaponType;

    private SoundObject _soundObject;


    public Collider AttackCollider { get { return attackCollider; } }
    public GameObject Gun { get { return gun; } }
    
    public void SetAnimation(Animator animator)
    {
        if(WeaponType == WeaponType.Sword)
        {
            CharacterAnimController.Sword(animator,true);
        }
        else if(WeaponType == WeaponType.AK)
        {
            CharacterAnimController.AK(animator, true);
        }
    }

    public void Shoot(Transform target)
    {
        GameObject _bullet = GameManager.Instance.Pools.Spawn(bulletPrefab, gun.transform.position, gun.transform.rotation);
        _bullet.transform.LookAt(target);
        if (shootParticlePrefab)
            GameManager.Instance.Pools.Spawn(shootParticlePrefab, gun.transform.position, gun.transform.rotation); 
        if(audio)
        {
            _soundObject = GameManager.Instance.CreateSoundObject();
            _soundObject.Play(audio, transform.position, 1f,1f, false);
        }
    }
}
[System.Serializable]
public enum WeaponType { Fist, Sword, AK }