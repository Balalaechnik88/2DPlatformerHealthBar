using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private MeleeAttack _attack;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerAnimationEventReceiver _eventReceiver;

    private void Awake()
    {
        if (_input == null)
            Debug.LogError($"[{nameof(PlayerAttacker)}] InputReader не назначен.", this);
        if (_attack == null)
            Debug.LogError($"[{nameof(PlayerAttacker)}] MeleeAttack не назначен.", this);
        if (_animator == null)
            Debug.LogError($"[{nameof(PlayerAttacker)}] PlayerAnimator не назначен.", this);
        if (_eventReceiver == null)
            Debug.LogError($"[{nameof(PlayerAttacker)}] PlayerAnimationEventReceiver не назначен.", this);
    }

    private void OnEnable()
    {
        if (_eventReceiver == null)
            return;

        _eventReceiver.HitEvent += OnHitEvent;
        _eventReceiver.AttackFinishedEvent += OnAttackFinishedEvent;
    }

    private void OnDisable()
    {
        if (_eventReceiver == null)
            return;

        _eventReceiver.HitEvent -= OnHitEvent;
        _eventReceiver.AttackFinishedEvent -= OnAttackFinishedEvent;
    }

    private void Update()
    {
        if (_input == null || _attack == null || _animator == null)
            return;

        if (_input.ConsumeAttackPressed() == false)
            return;

        if (_attack.TryStartAttack())
            _animator.TriggerAttack();
    }

    private void OnHitEvent()
    {
        _attack?.Hit();
    }

    private void OnAttackFinishedEvent()
    {
        _attack?.AttackFinished();
    }
}
