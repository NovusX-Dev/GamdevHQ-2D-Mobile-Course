using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainFollowVCam, _bossVCam;

    private void OnEnable()
    {
        LaughingWizard.onBossDeath += ChangePriorityToFollow;
    }

    private void OnDisable()
    {
        LaughingWizard.onBossDeath -= ChangePriorityToFollow;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _bossVCam.Priority = _mainFollowVCam.Priority + 1;
        }
    }

    private void ChangePriorityToFollow()
    {
        _bossVCam.Priority = _mainFollowVCam.Priority - 1;
    }
}
