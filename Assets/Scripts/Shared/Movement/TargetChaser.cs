using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TargetChaser : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 3.5f;

    private Rigidbody2D _rigidbody2D;
    public float CurrentSpeedX { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Stop()
    {
        CurrentSpeedX = 0f;
        _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
    }

    public void TickChase(Transform target)
    {
        if (target == null)
            return;

        float deltaXToTarget = target.position.x - transform.position.x;
        float directionX = Mathf.Sign(deltaXToTarget);

        CurrentSpeedX = directionX * _chaseSpeed;
        _rigidbody2D.velocity = new Vector2(CurrentSpeedX, _rigidbody2D.velocity.y);
    }
}
