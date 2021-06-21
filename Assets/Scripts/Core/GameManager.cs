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

    private bool _hasCastleKey = false;

    private void Awake()
    {
        _instance = this;
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

    public void CastleKeyStatus(bool active)
    {
        _hasCastleKey = active;
    }

    public bool GetCastleKey()
    {
        return _hasCastleKey;
    }

}
