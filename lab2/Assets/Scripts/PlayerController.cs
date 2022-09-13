using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float Jump = 5f;
    [SerializeField] private float _moveInput;
    [SerializeField] private float Speed = 5;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CollectibleType playerColor;


    void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveInput * Speed, rb.velocity.y);
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.AddForce(transform.up * Jump, ForceMode2D.Impulse);
        }
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectible collectible))
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
            Destroy(collectible.gameObject);
        }
    }


}