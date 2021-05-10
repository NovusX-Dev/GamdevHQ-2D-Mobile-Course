using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _diamonds;

    public void AddDiamonds(int amount)
    {
        _diamonds += amount;
    }

    public int GetDiamondAmount()
    {
        return _diamonds;
    }
}
