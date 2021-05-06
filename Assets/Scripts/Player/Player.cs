using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance;

    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] float _fallMultiplier = 2.5f, _lowJumpMultiplier = 2f;
    [SerializeField] Transform _groundPos;
    [SerializeField] LayerMask _groundMask;

    private float _xHorizontal;
    private bool _isGrounded;
    private bool _isJumping;

    public float XMove => _xHorizontal;
    public bool IsJUmping => _isJumping;
    public bool IsGrounded => _isGrounded;
    public bool IsGroundAttacking { get; set; }
   

    Rigidbody2D _rb2D;
    PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _instance = this;
        _rb2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }


    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundPos.position, 0.1f, _groundMask);

        if (!IsGroundAttacking)
        {
            Movement();
        }

        if (Input.GetButtonDown("Fire1") && _isGrounded)
        {
            _xHorizontal = 0;
            IsGroundAttacking = true;
            _playerAnimation.TriggerAttack();
        }

        BetterJumpingCalculation();
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(_xHorizontal * _moveSpeed, _rb2D.velocity.y);
    }

    private void Movement()
    {
        _xHorizontal = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb2D.velocity = Vector2.up * _jumpForce;
            }

            _isJumping = false;
        }
        else
        {
            _isJumping = true;
        }
    }


    private void BetterJumpingCalculation()
    {
        if (_rb2D.velocity.y < 0)
        {
            _rb2D.velocity += Vector2.up * Physics2D.gravity * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb2D.velocity += Vector2.up * Physics2D.gravity * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FlipPlayer()
    {
        if (_xHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_xHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }

}//class
