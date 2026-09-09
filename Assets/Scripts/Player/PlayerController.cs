using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;
    Vector2 direction;
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 8f;
    bool isGrounded = false;
    bool canMove = true;

    private void Reset()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        InputEvents.Move += OnMove;
        InputEvents.Jump += OnJump;
        GameStatesEvents.StateUpdated += OnStateUpdated;
    }

    private void OnStateUpdated(StateSO stateData)
    {
        canMove = stateData.CanMove;
    }

    private void OnMove(Vector2 dir)
    {
        direction = dir;
    }

    private void OnJump()
    {
        if (isGrounded && canMove)
        {
            rb2d.AddForceY(jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger(nameof(AnimationParameters.Jump));
        }
    }

    private void Update()
    {
        SetAnimations();
    }

    private void SetAnimations()
    {
        if(!canMove)
        {
            animator.SetFloat(nameof(AnimationParameters.DirectionX), 0);
            return;
        }
        animator.SetFloat(nameof(AnimationParameters.DirectionX), direction.x);
        if (rb2d.linearVelocityY < 0)
            animator.SetTrigger(nameof(AnimationParameters.Land));
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (!Mathf.Approximately(direction.x, 0f))
        {
            transform.localScale = new(Mathf.Sign(direction.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb2d.linearVelocityX = direction.x * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            SetGroundedAnimationParam();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            SetGroundedAnimationParam();
        }
    }

    private void SetGroundedAnimationParam()
    {
        animator.SetBool(nameof(AnimationParameters.IsGrounded), isGrounded);
    }

    enum AnimationParameters
    {
        DirectionX,
        Jump,
        Land,
        IsGrounded
    }

    private void OnDestroy()
    {
        InputEvents.Move -= OnMove;
        InputEvents.Jump -= OnJump;
    }
}