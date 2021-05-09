using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderAnimationEvent : MonoBehaviour
{
    EnemySpider _spider;

    private void Awake()
    {
        _spider = GetComponentInParent<EnemySpider>();
    }

    public void SpitAcid()
    {
        _spider.SpitAcid();
    }
}
