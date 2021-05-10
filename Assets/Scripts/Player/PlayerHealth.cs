using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    [HideInInspector] public int Health { get; set; }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    
    public void Damage(int damageAmount)
    {
        _currentHealth--;

        if (_currentHealth < 1)
        {
            Destroy(gameObject);
        }
    }

}//class
