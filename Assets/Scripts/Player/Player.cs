using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] float _fallMultiplier = 2.5f, _lowJumpMultiplier = 2f;
    [SerializeField] Transform _groundPos;
    [SerializeField] LayerMask _groundMask;

    private float _xHorizontal;
    private bool _isGrounded;

    public float XMove => _xHorizontal;

    Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundPos.position, 0.1f, _groundMask);

        _xHorizontal = Input.GetAxis("Horizontal");

        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb2D.velocity = Vector2.up * _jumpForce;
            }
        }

        BetterJumpingCalculation();
        FlipPlayer();
    }
    private void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(_xHorizontal * _moveSpeed, _rb2D.velocity.y);
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
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_xHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
