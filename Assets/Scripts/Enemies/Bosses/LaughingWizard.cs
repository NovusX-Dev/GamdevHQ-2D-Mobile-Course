using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingWizard : MonoBehaviour, IDamageable
{
    public delegate void OnBossDeath();

    public static event OnBossDeath onBossDeath;

    [SerializeField] int _health = 6;
    [SerializeField] HitBoxAttack _attackBox;
    [SerializeField] Diamond _diamondPrefab;
    [SerializeField] int _diamondsValue = 50;

    private bool _isFlipped = false;
    private bool _isEnraged = false;

    public int Health { get; set; }

    Animator _animator;
    PlayerStats _player;
    BoxCollider2D _collider;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Health = _health;
        _player = PlayerStats.Instance.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (DistanceCalculator(_player.transform) < 30f)
        {
            _animator.SetBool("playerNear", true);
        }

        _animator.SetBool("enraged", _isEnraged);
    }


    public void Damage(int damageAmount)
    {
        EnableCollider(0.1f, false);
        Health -= damageAmount;
        _animator.SetTrigger("damage");

        if (Health < 2)
        {
            _isEnraged = true;
            _attackBox.SetAttackPower(2);
        }

        if (Health < 1)
        {
            _animator.SetTrigger("die");
            Die();
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        InstantiateDiamond();
        onBossDeath?.Invoke();
    }

    public void LookAtPlayer()
    {
        var flipped = transform.localScale;
        flipped.z *= -1;

        if (transform.position.x > _player.transform.position.x && _isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < _player.transform.position.x && !_isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }

    private float DistanceCalculator(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);
    }

    public void EnableCollider(float time, bool enabled)
    {
        StartCoroutine(ColliderRoutine(time, enabled));
    }

    IEnumerator ColliderRoutine(float time, bool enabled)
    {
        yield return new WaitForSeconds(time);
        _collider.enabled = enabled;
    }


    public bool IsEnraged()
    {
        return _isEnraged;
    }

    private void InstantiateDiamond()
    {
        var diamond = Instantiate(_diamondPrefab.gameObject, transform.position, Quaternion.identity);
        diamond.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        diamond.transform.localScale *= 2;
        diamond.GetComponent<Diamond>().SetDiamondValue(_diamondsValue);
    }
}
