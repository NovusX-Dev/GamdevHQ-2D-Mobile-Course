using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private string _targetScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadingData.sceneToLoad = _targetScene;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        SavingSystem.Instance.SaveCurrency();
        yield return GameManager.Instance.EndLevel(2f);
        SceneManager.LoadScene("Loading Scene");
    }
}
