using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void Update()
    {
        
    }
}
