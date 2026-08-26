using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;
    Vector2 direction;
    [SerializeField] float speed = 50;
    [SerializeField] float jumpForce = 8f;
    bool isGrounded = false;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InputEvents.Move += OnMove;
        InputEvents.Jump += OnJump;
    }

    private void OnMove(Vector2 dir)
    {
        direction = dir;
    }

    private void OnJump()
    {
        if (isGrounded)
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