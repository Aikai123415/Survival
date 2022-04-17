using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class HostileCreatures : MonoBehaviour
{
    [Header("-----Stats-----")]
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected Image imgHealth;
    [SerializeField] protected Text txtHealth;
    protected float currentHealth;

    [Header("-----Other-----")]
    [SerializeField] protected PlayerController player;
    protected NavMeshAgent _agent;
    protected Animator _animator;
    
    protected void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    abstract public void Die();
    abstract protected void Update();
    abstract public void ApplyDamage(float damage);
   
}
