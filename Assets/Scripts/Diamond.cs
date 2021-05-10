using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private int _value = 1;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
