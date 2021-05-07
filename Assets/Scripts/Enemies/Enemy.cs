using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _moveSpeed;
    [SerializeField] protected int _gems;
    [SerializeField] protected Transform _pointA, _pointB;
    [SerializeField] protected Transform _waypointsParent;

    protected Vector3 _targetWaypoint;
    protected bool _facingRight;
    protected bool _canFlip;

    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        _pointA.parent = _waypointsParent;
        _pointB.parent = _waypointsParent;
        _facingRight = (_pointA.position.x - _pointB.position.x) > 0;
    }

    protected virtual void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("idle")) return;
        Movement();
    }

    protected virtual void Movement()
    {
        if (_canFlip) FlipEnemy();
        _canFlip = false;
        var step = _moveSpeed * Time.deltaTime;

        if (transform.position == _pointA.position)
        {
            _canFlip = true;
            _targetWaypoint = _pointB.position;
            _animator.SetTrigger("idle");
        }
        else if (transform.position == _pointB.position)
        {
            _canFlip = true;
            _targetWaypoint = _pointA.position;
            _animator.SetTrigger("idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint, step);
    }

    protected virtual void FlipEnemy()
    {
        if (Vector3.Distance(transform.position, _pointA.position) < 0.15f)
        {
            transform.localScale = _facingRight ? Vector3.one : new Vector3(-1, 1, 1);
        }
        else if (Vector3.Distance(transform.position, _pointB.position) < 0.15f)
        {
            transform.localScale = _facingRight ? new Vector3(-1, 1, 1) : Vector3.one;
        }
    }


    public virtual void Attack()
    {

    }

}//class
