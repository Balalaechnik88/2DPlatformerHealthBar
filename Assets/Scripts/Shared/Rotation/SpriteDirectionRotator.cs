using UnityEngine;

public class SpriteDirectionRotator : MonoBehaviour
{
    private const float MinAbsoluteDirection = 0.01f;

    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _rightRotation = Quaternion.Euler(0f, 0f, 0f);
        _leftRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void SetFacingDirection(float directionX)
    {
        if (Mathf.Abs(directionX) < MinAbsoluteDirection)
            return;

        transform.rotation = directionX > 0f ? _rightRotation : _leftRotation;
    }
}
