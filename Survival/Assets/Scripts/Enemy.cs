using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HostileCreatures
{
    [Header("Idle")]
    [SerializeField] protected Transform[] walkingPoints;
    [SerializeField] protected float timeBeforeNextPoint;
    protected float _currentTimeBeforeNextPoint;
    protected bool targetIsAssigned = false;

    [Header("Chasing")]
    [SerializeField] protected float delayBeforeChasingPlayer;
    protected float _currentDelayBeforeChasingPlayer;

    [Header("Attack")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected Transform centerOfAttack;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected float timeBetweenAttack;
    [SerializeField] protected float damage;
    [SerializeField] protected float radius;
    protected float _currentTimeBetweenAttack;

    [Header("Find")]

    [Header("Other")]
    [SerializeField] protected float rewardForDie;

    protected enum EnemyBehaviour { Idle, Chase, Attack, Found }
    protected EnemyBehaviour _enemyBehaviour;
    protected override void Update()
    {
        switch (_enemyBehaviour) 
        {
            case EnemyBehaviour.Idle:
                Idle();
                break;
            case EnemyBehaviour.Found:
                Found();
                break;
            case EnemyBehaviour.Chase:
                Chase();
                break;
            case EnemyBehaviour.Attack:
                Attack();
                break;
        }
    }
    protected virtual void Found()
    {
        
    }
    protected virtual void Idle()
    {
        if (targetIsAssigned==false)
        {
            _agent.SetDestination(walkingPoints[Random.Range(0, walkingPoints.Length)].position);
            targetIsAssigned = true;
        }
        while(_agent.remainingDistance <= 0.1f)
        {
            _currentTimeBeforeNextPoint -= Time.deltaTime;
            if (_currentTimeBeforeNextPoint <= 0)
            {
                _currentTimeBeforeNextPoint = timeBeforeNextPoint;
                targetIsAssigned = false;
                return;
            }
        }
    }
    protected virtual void Chase()
    {

    }
    protected void PreparingForTheChase()
    {

    }
    protected virtual void Attack()
    {
        _currentTimeBetweenAttack -= Time.deltaTime;
        if (_agent.remainingDistance < distanceToAttack && _currentTimeBetweenAttack <= 0) 
        {
            _animator.SetTrigger("Attack");
            _currentTimeBetweenAttack = timeBetweenAttack;
        }
        else if (_agent.remainingDistance < distanceToAttack)
        {
            _enemyBehaviour = EnemyBehaviour.Chase;
        }
    }
    public void Brunt()
    {
        var hostileColliders = Physics.OverlapSphere(centerOfAttack.position, radius, playerMask);
        for (int i = 0; i < hostileColliders.Length; i++)
        {
            hostileColliders[i].gameObject.GetComponent<PlayerController>().ApplyDamage(damage);
        }
    }
    public override void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            imgHealth.fillAmount = currentHealth / health;
            txtHealth.text = health.ToString("0");
            _animator.SetTrigger("Die");
        }
    }
    public override void Die()
    {
        //ResourcesManager.ChangeMoney(rewardForDie);
    }
}
