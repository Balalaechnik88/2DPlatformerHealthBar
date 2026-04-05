using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 7f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool TryJump(bool isGrounded, bool isJumpPressed)
    {
        if (isGrounded == false || isJumpPressed == false)
            return false;

        Vector2 velocity = _rigidbody2D.velocity;
        velocity.y = _jumpForce;
        _rigidbody2D.velocity = velocity;

        return true;
    }
}
