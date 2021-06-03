using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class KnightController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset Idle, Run, Jump, Attack1, Attack2;
    public string currentState;
    public float speed, jumpSpeed;
    public float movement;
    private Rigidbody2D rigidbody;
    public string currentAnimation;
    private Vector2 oriScale;
    bool onJump, onAttack;
    float jumpTime, attackTime;
    int attackcombo;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharaterState(currentState);
        oriScale = transform.localScale;
        onJump = false;
        onAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onJump)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime >= 1f)
            {
                onJump = false;
                jumpTime = 0;
            }
        }
        if (onAttack)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 1f)
            {
                onAttack = false;
                attackTime = 0;
                attackcombo = 0;
            }
        }

        Move();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(currentAnimation))
            return;
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timescale;
        currentAnimation = animation.name;
    }

    public void SetCharaterState(string state)
    {
        if (state.Equals("Run"))
        {
            SetAnimation(Run, true, 1f);
        }
        else if (state.Equals("Jump"))
        {
            SetAnimation(Jump, false, 1f);
        }
        else if (state.Equals("Attack1"))
        {
            SetAnimation(Attack1, false, 1f);
        }
        else if (state.Equals("Attack2"))
        {
            SetAnimation(Attack2, false, 1f);
        }
        else
        {
            SetAnimation(Idle, true, 1f);
        }

        currentState = state;
        
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");

        if(!onAttack)
            rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);

        if (movement != 0)
        {
            if(!onJump && !onAttack)
                SetCharaterState("Run");
            if (movement > 0)
                transform.localScale = new Vector2(oriScale.x, oriScale.y);
            else
                transform.localScale = new Vector2(-oriScale.x, oriScale.y);
            
        }
        else
        {
            if (!onJump && !onAttack)
                SetCharaterState("Idle");
        }
        if (Input.GetButtonDown("Jump") && onJump == false )
        {
            Jumping();
            onJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) && onJump == false)
        {
            Debug.Log("有觸發攻擊");
            onAttack = true;
            if (currentState == "Attack1")
            { 
                attackTime = 0;
                attackcombo += 1;
            }
            else if (currentState == "Attack2")
                attackcombo = 0;
            else
                attackcombo += 1;
            Attack();
        }

    }

    public void Jumping()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        SetCharaterState("Jump");
    }

    public void Attack()
    {
        if (attackcombo == 1)
            SetCharaterState("Attack1");
        else if (attackcombo == 2)
            SetCharaterState("Attack2");
    }
}
