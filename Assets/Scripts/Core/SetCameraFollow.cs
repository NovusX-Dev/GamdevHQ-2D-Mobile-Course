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
        _cam.Follow = GameObject.Find("Player Cam Follow").transform;
    }

}
