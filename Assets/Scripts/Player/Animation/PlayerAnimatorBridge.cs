using UnityEngine;

public class PlayerAnimationBridge : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _animator;

    private void Awake()
    {
        if (_mover == null)
            Debug.LogError($"[{nameof(PlayerAnimationBridge)}] PlayerMover не назначен.", this);
        if (_animator == null)
            Debug.LogError($"[{nameof(PlayerAnimationBridge)}] PlayerAnimator не назначен.", this);
    }

    private void Update()
    {
        if (_mover == null || _animator == null)
            return;

        _animator.SetSpeed(Mathf.Abs(_mover.CurrentSpeedX));
        _animator.SetGrounded(_mover.IsGrounded);
    }
}
