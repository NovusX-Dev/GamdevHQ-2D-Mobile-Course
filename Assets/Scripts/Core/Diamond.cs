using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] AudioClip _clip;

    private int _value = 1;

    Animator _anim;
    AudioSource _audioSource;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(_clip);
            other.GetComponent<PlayerStats>().AddDiamonds(_value);
            Destroy(gameObject);
        }
    }

    public void SetDiamondValue(int value)
    {
        _anim.SetTrigger("bounce");
        _value = value;
    }

    
}
