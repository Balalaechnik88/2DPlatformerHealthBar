using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHorizontalMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    public float CurrentSpeedX => _rigidbody2D != null ? _rigidbody2D.velocity.x : 0f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ApplyHorizontal(float horizontalInput)
    {
        Vector2 velocity = _rigidbody2D.velocity;
        velocity.x = horizontalInput * _moveSpeed;
        _rigidbody2D.velocity = velocity;
    }
}
