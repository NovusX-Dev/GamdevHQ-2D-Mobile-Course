using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _swordAnim;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    void Update()
    {
        _anim.SetFloat("xMove", Mathf.Abs(Player.Instance.XMove));
        _anim.SetBool("jumping", Player.Instance.IsJUmping);
    }

    public void TriggerAttack()
    {
        _anim.SetTrigger("attack");
        _swordAnim.SetTrigger("swordArcAnim");
    }
}
