using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const string SpeedParameterName = "Speed";
    private const string AttackTriggerName = "Attack";

    [SerializeField] private Animator _animator;

    private int _speedId;
    private int _attackId;

    private bool _hasSpeedParameter;
    private bool _hasAttackTrigger;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        _speedId = Animator.StringToHash(SpeedParameterName);
        _attackId = Animator.StringToHash(AttackTriggerName);

        _hasSpeedParameter = HasParameter(_animator, _speedId);
        _hasAttackTrigger = HasParameter(_animator, _attackId);
    }

    public void PlayAttack()
    {
        if (_animator == null || _hasAttackTrigger == false)
            return;

        _animator.SetTrigger(_attackId);
    }

    public void SetSpeed(float speed)
    {
        if (_animator == null || _hasSpeedParameter == false)
            return;

        _animator.SetFloat(_speedId, speed);
    }

    private static bool HasParameter(Animator animator, int parameterId)
    {
        if (animator == null)
            return false;

        AnimatorControllerParameter[] parameters = animator.parameters;

        for (int index = 0; index < parameters.Length; index++)
        {
            if (parameters[index].nameHash == parameterId)
                return true;
        }

        return false;
    }
}
