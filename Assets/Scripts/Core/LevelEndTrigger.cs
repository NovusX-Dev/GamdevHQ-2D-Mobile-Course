using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private string _targetScene;
    [SerializeField] bool _needKey = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadingData.sceneToLoad = _targetScene;
            if (_needKey && GameManager.Instance.GetCastleKey())
            {
                StartCoroutine(LoadNextLevel());
                GameManager.Instance.CastleKeyStatus(false);
            }
            else if (_needKey && !GameManager.Instance.GetCastleKey())
            {
                StartCoroutine(
                    UIManager.Instance.ActivateMessagePanel("You do not have the key! Buy it from the Shop Keeper"));
            }
            else if (!_needKey)
            {
                StartCoroutine(LoadNextLevel());
            }
        }
    }

    IEnumerator LoadNextLevel()
    {
        SavingSystem.Instance.SaveCurrency();
        yield return GameManager.Instance.EndLevel(1f);
        SceneManager.LoadScene("Loading Scene");
    }

}
