using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingWizard : MonoBehaviour, IDamageable
{
    public delegate void OnBossDeath();
    public static event OnBossDeath onBossDeath;

    private enum BossStates
    {
        Idle,
        Attack,
        Damage,
        Death
    };
    private BossStates _bossStates;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] int _health = 6;
    [SerializeField] int _attackPower;

    public int Health { get; set; }

    void Start()
    {
        Health = _health;
    }

    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;

        if (Health < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        onBossDeath?.Invoke();
    }

   
    
}
