using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    [SerializeField] private int _attackPower = 1;

    private bool _canAttack;
    private int _currentAttackPower;

    private void OnEnable()
    {
        _canAttack = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.GetComponent<IDamageable>();

        if (hit != null && _canAttack)
        {
            hit.Damage(_attackPower);
            _canAttack = false;
        }
    }

    public void SetAttackPower(int newPower)
    {
        _attackPower = newPower;
    }
}
