using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    #region Variables
    [Header("Attributes")]
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;

    [Header("Diamond")] 
    [SerializeField] protected Diamond _diamondPrefab;
    [SerializeField] protected int _diamondsValue;

    [Header("Attack")]
    [SerializeField] protected float _attackDistance = 3f;

    [Header("Movement")]
    [SerializeField] protected Transform _waypointsParent;
    [SerializeField] protected Transform _pointA, _pointB;
    
    protected Vector3 _targetWaypoint;
    protected float _distanceToPlayer;
    protected bool _facingRight;
    protected bool _canFlip;
    protected bool _isHit = false;
    protected bool _isWalking = false;
    protected bool _inCombat = false;
    protected bool _isDead = false;

    #endregion

    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Init()
    {
        _pointA.parent = _waypointsParent;
        _pointB.parent = _waypointsParent;
        _facingRight = (_pointA.position.x - _pointB.position.x) < 0;
    }

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (_isDead) return;

        CheckDistanceToPlayer();
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

        if (!_isHit && !_inCombat)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint, step);
        }

        
    }

    protected virtual void FlipEnemy()
    {
        if (!_inCombat)
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
    }

    protected virtual void Attack()
    {
        _inCombat = true;
    }

    protected virtual void CheckDistanceToPlayer()
    {
        var player = Player.Instance;

        if (player != null && !player.IsDead)
        {
            _distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (_distanceToPlayer > _attackDistance)
            {
                _inCombat = false;
            }
            else
            {
                Attack();
                var direction = player.transform.localPosition - transform.localPosition;
                transform.localScale = direction.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
            }
        }
        else
        {
            _inCombat = false;
        }

        _animator.SetBool("inCombat", _inCombat);
    }

    public virtual void ResetHitBool()
    {
        _isHit = false;
    }

    public virtual void CanMoveAgain()
    {
        _isWalking = true;
    }

    public virtual bool GetIsHit()
    {
        return _isHit;
    }

    protected virtual void InstantiateDiamond()
    {
        var diamond = Instantiate(_diamondPrefab.gameObject, transform.position, Quaternion.identity);
        diamond.GetComponent<Diamond>().SetDiamondValue(_diamondsValue);
    }
    
}//class
