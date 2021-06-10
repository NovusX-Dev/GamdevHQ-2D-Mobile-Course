using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraFollow : MonoBehaviour
{
    CinemachineVirtualCamera _cam;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        FollowPlayer();
    }

    private void LateUpdate()
    {
        if (!Player.Instance.IsDead)
        {
            FollowPlayer();
        }
    }

    public void FollowPlayer()
    {
        _cam.Follow = GameObject.Find("Player Cam Follow").transform;
    }

}
