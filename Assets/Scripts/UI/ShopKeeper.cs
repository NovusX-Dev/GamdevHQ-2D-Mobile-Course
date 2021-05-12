using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] GameObject _shopPanel;

    private int _currentSelectedItem;
    private int _itemCost;

    void Update()
    {
        if (_shopPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                UIManager.Instance.SelectionBarStatus(false);
                _itemCost = 0;
                _shopPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _shopPanel.SetActive(true);
            UIManager.Instance.UpdateShopDiamonds(other.GetComponent<PlayerStats>().GetDiamondAmount());
            Time.timeScale = 0f;
        }
    }

    public void SelectItem(int selectedItem)
    {
        //0 = flame sword
        //1 = boots of flight
        //2 = key
        _currentSelectedItem = selectedItem;

        switch (selectedItem)
        {
            case 0: 
                UIManager.Instance.UpdateShopSelectionBar(90);
                _itemCost = 300;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelectionBar(-25);
                _itemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelectionBar(-146);
                _itemCost = 150;
                break;
        }

        
    }

    public void BuyItem()
    {
        var playerDiamonds = PlayerStats.Instance.GetDiamondAmount();
        if (playerDiamonds>= _itemCost)
        {
            PlayerStats.Instance.DeductDiamonds(_itemCost);
        }
        else
        {
            Debug.Log("Cannot buy item");
        }
        
    }

}//class