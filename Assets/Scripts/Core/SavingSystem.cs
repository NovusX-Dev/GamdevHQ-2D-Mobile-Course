using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    private static SavingSystem _instance;
    public static SavingSystem Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No SavingSystem Found");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        LoadCurrency();
    }

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt("Diamonds", PlayerStats.Instance.GetDiamondAmount());
    }

    public void LoadCurrency()
    {
        PlayerStats.Instance.AddDiamonds(PlayerPrefs.GetInt("Diamonds"));
        UIManager.Instance.UpdateHUDDiamonds(PlayerPrefs.GetInt("Diamonds"));
    }
}
