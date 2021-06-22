using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _swordAnim;
    [SerializeField] AudioClip _swordSFX;

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

    public void TriggerAirAttack()
    {
        _anim.SetTrigger("airAttack");
        _swordAnim.SetTrigger("airArc");
    }

    public void TriggerDeath()
    {
        _anim.SetTrigger("die");
    }

    public void TriggerHurt()
    {
        _anim.SetTrigger("hurt");
    }

    public void PlaySwordSFX()
    {
        AudioSource.PlayClipAtPoint(_swordSFX, Camera.main.transform.position);
    }
}
