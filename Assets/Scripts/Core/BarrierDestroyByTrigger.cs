using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroyByTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem _destroyVFX;

    private void OnEnable()
    {
        LaughingWizard.onBossDeath += DestroyObject;
    }

    private void OnDisable()
    {
        LaughingWizard.onBossDeath -= DestroyObject;
    }

    private void DestroyObject()
    {
        var vfx = Instantiate(_destroyVFX, gameObject.transform);
        Destroy(vfx, 2f);
        Destroy(gameObject, 2.25f);
    }

}
