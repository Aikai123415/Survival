using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("------Stats------")]
    [SerializeField] protected float expendedEnergy;
    [SerializeField] protected float radius;
    [SerializeField] protected float damage;
    [SerializeField] protected float agility;
    [SerializeField] protected float timeBtwAttack;
    protected float _currentTimeBtwAttack;
    [Header("-------Other------")]
    [SerializeField] protected Transform centerOfAttack;
    [SerializeField] protected Animator playerAnimator;
    [SerializeField] protected PlayerEnergy playerEnergy;

    abstract public void Attack();
}
