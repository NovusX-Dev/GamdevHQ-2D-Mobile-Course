using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("Death Panel")]
    [SerializeField] GameObject _deathPanel;
    [SerializeField] string _mainMenuString;

    [Header("Message Panel")]
    [SerializeField] GameObject _messagePanel;
    [SerializeField] TextMeshProUGUI _messageText;

    [Header("References")]
    [SerializeField] CanvasGroup _faderCanvasGroup;

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
        _shopDiamondValue.text = $"{value} G";
    }

    public void UpdateLivesDisplay(int remainingLives)
    {
        for(int i = 0; i < _lives.Length; i++)
        {
            if (i == (remainingLives))
            {
                _lives[i].SetActive(false);
                if (i + 1 < _lives.Length && _lives[i+1] != null)
                {
                    _lives[i + 1].SetActive(false);
                }
            }
        }
       
    }

    #region Shop Panel
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

    public void DeactivateShop()
    {
        _shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool IsShopPanelActive()
    {
        return _shopPanel.activeInHierarchy;
    }
    #endregion

    #region Death panel
    public void ActivateDeathPanel()
    {
        _deathPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        LoadingData.sceneToLoad = _mainMenuString;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Message Panel

    public  IEnumerator ActivateMessagePanel(string message)
    {
        _messagePanel.SetActive(true);
        _messageText.text = message;
        yield return new WaitForSeconds(3f);
        _messageText.text = "";
        _messagePanel.SetActive(false);
    }

    #endregion

    #region Fader
    public void FadeOutImmediate()
    {
        _faderCanvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(float time)
    {
        while (_faderCanvasGroup.alpha < 1)
        {
            _faderCanvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (_faderCanvasGroup.alpha > 0)
        {
            _faderCanvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
    #endregion
}
