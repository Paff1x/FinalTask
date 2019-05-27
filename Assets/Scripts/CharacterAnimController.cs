using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController
{
    public static void FistAttack(Animator animator)
    {
        animator.SetTrigger("FistAttack");
    }

    public static void SwordAttack(Animator animator)
    {
        animator.SetTrigger("SwordAttack");
    }

    public static void Walk(Animator animator, bool flag)
    {
        animator.SetBool("Walk", flag);
    }

    public static void SpeedWalk(Animator animator, float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public static void Sword(Animator animator, bool flag)
    {
        animator.SetBool("Sword", flag);
    }

    public static void AK(Animator animator, bool flag)
    {
        animator.SetBool("AK", flag);
    }

    public static void Shoot(Animator animator)
    {
        animator.SetTrigger("Shoot");
    }
}
