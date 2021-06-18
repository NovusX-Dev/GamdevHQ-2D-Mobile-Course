using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance => _instance;

    [Header ("Movement")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] float _fallMultiplier = 2.5f, _lowJumpMultiplier = 2f;

    [Header("References")]
    [SerializeField] Transform _groundPos;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] Transform _respawnPosition;
    

    private float _xHorizontal;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isDead;

    public float XMove => _xHorizontal;
    public bool IsJUmping => _isJumping;
    public bool IsGrounded => _isGrounded;
    public bool IsGroundAttacking { get; set; }
    public bool IsDead => _isDead;
   

    Rigidbody2D _rb2D;
    PlayerAnimation _playerAnimation;
    SetCameraFollow _virtualCamera;

    private void Awake()
    {
        _instance = this;
        _rb2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    private void Start()
    {
        _virtualCamera = GameObject.Find("CM Follow Cam").GetComponent<SetCameraFollow>();
    }

    void Update()
    {
        if (_isDead) return;

        _isGrounded = Physics2D.OverlapCircle(_groundPos.position, 0.1f, _groundMask);

        if (!IsGroundAttacking)
        {
            Movement();
        }

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && _isGrounded)
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
        //_xHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump") || CrossPlatformInputManager.GetButton("B_Button"))
            {
                _rb2D.velocity = Vector2.up * _jumpForce;
            }

            _isJumping = false;
        }
        else
        {
            _isJumping = true;
            if (CrossPlatformInputManager.GetButtonDown("A_Button") && _isJumping)
            {
                _playerAnimation.TriggerAirAttack();
            }
        }
    }


    private void BetterJumpingCalculation()
    {
        if (_rb2D.velocity.y < 0)
        {
            _rb2D.velocity += Vector2.up * Physics2D.gravity * (_fallMultiplier - 1) * Time.deltaTime;
        }
        /*else if (_rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb2D.velocity += Vector2.up * Physics2D.gravity * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }*/
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

    public void PlayerDeath(PlayerStats stats)
    {
        Time.timeScale = 0.5f;
        _xHorizontal = 0f;
        _isDead = true;
        stats.enabled = false;
    }

    public void PlayerRespawn()
    {
        _isDead = true;
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return UIManager.Instance.FadeOut(0.25f);
        Time.timeScale = 0.25f;

        _xHorizontal = 0f;
        transform.position = _respawnPosition.position;

        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        _isDead = false;
        _virtualCamera.FollowPlayer();
        yield return UIManager.Instance.FadeIn(2f);
        
    }
}//class
