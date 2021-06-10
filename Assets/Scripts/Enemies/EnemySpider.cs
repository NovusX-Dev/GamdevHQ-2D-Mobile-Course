using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy, IDamageable
{
    [Header("Acid Attack")]
    [SerializeField] GameObject _acidPrefab;
    [SerializeField] Transform _attackPos;

    public int Health { get; set; }

    protected override void Init()
    {
        base.Init();
        Health = _health;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        _isHit = true;

        if (Health < 1)
        {
            GetComponent<Collider2D>().enabled = false;
            _isDead = true;
            _animator.SetTrigger("die");
            InstantiateDiamond();
            Destroy(gameObject, 30f);
        }
    }

    protected override void Attack()
    {
        base.Attack();
    }


    public void SpitAcid()
    {
        var acid = Instantiate(_acidPrefab, _attackPos.position, Quaternion.identity);
        //Instantiate(_acidPrefab, _attackPos.position, Quaternion.identity);
        acid.GetComponent<AcidAttack>().SetDirection(transform.localScale.x >= 1f ? Vector2.right : Vector2.left);
    }


}//class
