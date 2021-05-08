using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    protected override void Init()
    {
        base.Init();
        Health = _health;
    }

    public void Damage(int damageAmount)
    {
        Health--;
        _animator.SetTrigger("hit");
        _isHit = true;

        if (Health < 1)
        {
            GetComponent<Collider2D>().enabled = false;
            _isDead = true;
            _animator.SetTrigger("die");
            Destroy(gameObject, 30f);
        }
    }

}//class
