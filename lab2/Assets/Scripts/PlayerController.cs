using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float JumpForce = 8f;

    [SerializeField] private float Speed = 5;
    [SerializeField] private PlayerAnimatorController animatorController;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float coyoteCount;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollectibleType playerColor;

    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckDistance = 0.01f;


    private bool _isGrounded;
    private float _moveInput;


    private void Update()
    {
        CheckGround();
        SetAnimatorParameter();
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveInput * Speed, rb.velocity.y);
    }


    public void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }


    private void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        
        TryJumping();
    
    }


    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<float>();

        FlipPlayerSprite();
    }


    private void CheckCoyotetime()
    {
        if (_isGrounded)
        {
            coyoteCount = coyoteTime;
        }
        else
        {
            coyoteCount =- Time.deltaTime;
        }
    } 


    private void FlipPlayerSprite()
    {
        if (_moveInput < 0)
        {
            transform.localScale = new Vector3(x:-1, y:1, z:1);
        }
        else if (_moveInput > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    
    private void TryJumping()
    {
        if (!_isGrounded) return;

        Jump(JumpForce);
    }


    private void CheckGround()
    {
    
        var playerBounds = playerCollider.bounds;

        RaycastHit2D raycastHit = Physics2D.BoxCast(
            playerBounds.center,
            playerBounds.size,
            0f, 
            Vector2.down, 
            groundCheckDistance, 
            groundLayers);
                

        _isGrounded = raycastHit.collider != null;
               
    }


    private void SetAnimatorParameter()
    {
        animatorController.SetAnimatorParameter(rb.velocity, _isGrounded);
    }


    private void TakeDamage()
    {
        GameManager.instance.ProcessPlayerDeath();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.TryGetComponent(out Collectible collectible))
        {
            CollectibleType playerColor = collectible.color;

            switch (playerColor)
            {
                case CollectibleType.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case CollectibleType.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case CollectibleType.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
            }
            collision.gameObject.SetActive(false);

        }*/
        if (collision.CompareTag("Finish"))
        {
            GameManager.instance.LoadNextLevel();
        }
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            TakeDamage();
        }
    }
}