using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    [SerializeField] private int _diamonds;

    private void Awake()
    {
        _instance = this;    
    }

    public void AddDiamonds(int amount)
    {
        _diamonds += amount;
    }

    public void DeductDiamonds(int amount)
    {
        _diamonds -= amount;
        if (UIManager.Instance.IsShopPanelActive())
        {
            UIManager.Instance.UpdateShopDiamonds(_diamonds);
        }
    }

    public int GetDiamondAmount()
    {
        return _diamonds;
    }
}
