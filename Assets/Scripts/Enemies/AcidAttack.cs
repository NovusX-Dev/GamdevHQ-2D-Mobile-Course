using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidAttack : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _attackPower;

    private Vector2 _currentDirection;

    private void Start()
    {
        Destroy(gameObject, 6f);
    }

    private void Update()
    {
        transform.Translate(_currentDirection * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().Damage(_attackPower);
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;
    }
    
}
