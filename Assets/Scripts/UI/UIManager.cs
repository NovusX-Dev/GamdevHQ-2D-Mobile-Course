using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("HUD")]
    [SerializeField] TextMeshProUGUI _diamondValue;
    [SerializeField] GameObject[] _lives = null;

    [Header("Shop Keeper")]
    [SerializeField] GameObject _shopPanel;
    [SerializeField] TextMeshProUGUI _shopDiamondValue;
    [SerializeField] Image _selectionImage;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SelectionBarStatus(false);
        UpdateHUDDiamonds(PlayerStats.Instance.GetDiamondAmount());
    }


    public void UpdateHUDDiamonds(int amount)
    {
        _diamondValue.text = amount.ToString();
    }

    public void UpdateShopDiamonds(int value)
    {
        _shopDiamondValue.text = $"{value}G";
    }

    public void UpdateLivesDisplay(int remaningLives)
    {
        for(int i = 0; i < _lives.Length; i++)
        {
            if (i == (remaningLives))
            {
                _lives[i].SetActive(false);
            }
        }
       
    }

    public void UpdateShopSelectionBar(int yPos)
    {
        if (!_selectionImage.gameObject.activeInHierarchy)
        {
            SelectionBarStatus(true);
        }

        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x,
            yPos);
    }

    public void SelectionBarStatus(bool active)
    {
        _selectionImage.gameObject.SetActive(active);
    }

    public bool IsShopPanelActive()
    {
        return _shopPanel.activeInHierarchy;
    }
}
