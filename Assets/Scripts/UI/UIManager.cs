using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("Shop Keeper")]
    [SerializeField] GameObject _shopPanel;
    [SerializeField] TextMeshProUGUI _diamondValue;
    [SerializeField] Image _selectionImage;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SelectionBarStatus(false);
    }

    public void UpdateShopDiamonds(int value)
    {
        _diamondValue.text = $"{value}G";
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
