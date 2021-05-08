using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy, IDamageable
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

        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }

}//class
