using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDirectionFlipper : MonoBehaviour
{
    private const float MinAbsoluteDirection = 0.01f;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _invert;

    private void Awake()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer == null)
        {
            Debug.LogError($"[{nameof(SpriteDirectionFlipper)}] Не найден SpriteRenderer. Скрипт отключён.", this);
            enabled = false;
        }
    }

    public void SetFacingDirection(float directionX)
    {
        if (enabled == false)
            return;

        if (Mathf.Abs(directionX) < MinAbsoluteDirection)
            return;

        bool shouldFlip = directionX < 0f;

        if (_invert)
            shouldFlip = !shouldFlip;

        _spriteRenderer.flipX = shouldFlip;
    }
}
