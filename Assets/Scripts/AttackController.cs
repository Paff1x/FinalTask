using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AttackController
{
    public static void Attack(this WeaponPlace weaponPlace, Animator animator)
    {
        if (weaponPlace.CurrentWeapon.WeaponType == WeaponType.Fist)
        {
            CharacterAnimController.FistAttack(animator);
        }
        else if(weaponPlace.CurrentWeapon.WeaponType == WeaponType.Sword)
        {
            CharacterAnimController.SwordAttack(animator);
        }
        else if(weaponPlace.CurrentWeapon.WeaponType == WeaponType.AK)
        {
            CharacterAnimController.Shoot(animator);
        }

    }
}
