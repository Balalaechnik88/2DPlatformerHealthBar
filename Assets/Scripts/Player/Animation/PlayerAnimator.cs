using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string SpeedName = "Speed";
    private const string IsGroundedName = "IsGrounded";
    private const string AttackTriggerName = "Attack";

    private Animator _animator;

    private int _speedId;
    private int _groundedId;
    private int _attackId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _speedId = Animator.StringToHash(SpeedName);
        _groundedId = Animator.StringToHash(IsGroundedName);
        _attackId = Animator.StringToHash(AttackTriggerName);
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool(_groundedId, isGrounded);
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_speedId, speed);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(_attackId);
    }
}
