using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWalkBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackDistance = 5f;
    LaughingWizard _boss;
    Transform _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _boss = animator.GetComponent<LaughingWizard>();
        
        _boss.EnableCollider(2f, true);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();

        var _step = _moveSpeed * Time.deltaTime;
        var target = new Vector2(_player.position.x, _boss.transform.position.y);

        _boss.transform.position = Vector3.MoveTowards(_boss.transform.position, target, _step);

        if (Vector3.Distance(_boss.transform.position, _player.position) < _attackDistance)
        {
            if (!_boss.IsEnraged())
            {
                animator.SetTrigger("attack");
            }
            else
            {
                animator.SetTrigger("enragedAttack");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.ResetTrigger("enragedAttack");
    }

}
