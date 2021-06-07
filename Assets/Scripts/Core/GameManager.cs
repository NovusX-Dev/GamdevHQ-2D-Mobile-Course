using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No GameManager Found");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt("Diamonds", PlayerStats.Instance.GetDiamondAmount());
    }

    public void LoadCurrency()
    {
        PlayerPrefs.GetInt("Diamonds");
    }

    public void PlayerDeadlyRespwan()
    {
        Player.Instance.PlayerRespawn();
    }

    public IEnumerator EndLevel(float time)
    {
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
    }
}