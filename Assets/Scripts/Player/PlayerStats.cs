using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    [SerializeField] private int _diamonds;
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private bool _isHurt;

    [HideInInspector] public int Health { get; set; }

    PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _instance = this;
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_isHurt) return;

        _currentHealth -= damageAmount;

        if(_currentHealth > 1)
        {
            _playerAnimation.TriggerHurt();
            StartCoroutine(HurtRoutine());
        }

        UIManager.Instance.UpdateLivesDisplay(_currentHealth);

        if (_currentHealth < 1)
        {
            UIManager.Instance.UpdateLivesDisplay(_currentHealth);
            _playerAnimation.TriggerDeath();
            Player.Instance.PlayerDeath(this);
        }
    }
    
    public void AddDiamonds(int amount)
    {
        _diamonds += amount;
        UIManager.Instance.UpdateHUDDiamonds(_diamonds);
    }

    public void DeductDiamonds(int amount)
    {
        _diamonds -= amount;
        if (UIManager.Instance.IsShopPanelActive())
        {
            UIManager.Instance.UpdateShopDiamonds(_diamonds);
        }
        UIManager.Instance.UpdateHUDDiamonds(_diamonds);
    }

    public int GetDiamondAmount()
    {
        return _diamonds;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    IEnumerator HurtRoutine()
    {
        _isHurt = true;
        yield return new WaitForSeconds(1f);
        _isHurt = false;
    }

}
