using System;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private WaypointPatroller _patroller;
    [SerializeField] private TargetChaser _chaser;
    [SerializeField] private MeleeAttack _attack;
    [SerializeField] private SpriteDirectionFlipper _directionFlipper;
    [SerializeField] private FacingPointsMirror _pointsMirror;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyAnimationEventReceiver _eventReceiver;

    [Header("Attack")]
    [SerializeField] private float _attackStartDistance = 2f;

    private AttackRangeChecker _attackRangeChecker;
    private Transform _currentTarget;
    private bool _isAttacking;
    private IEnemyStrategy _patrolStrategy;

    public event Action AttackStarted;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        if (_detector == null ||
            _patroller == null ||
            _chaser == null ||
            _attack == null ||
            _directionFlipper == null ||
            _pointsMirror == null ||
            _animator == null ||
            _eventReceiver == null)
        {
            Debug.LogError($"[{nameof(EnemyBrain)}] Не назначены обязательные ссылки. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        _attackRangeChecker = new AttackRangeChecker(_attackStartDistance);
        _patrolStrategy = new EnemyPatrolStrategy(_patroller, _chaser);
    }

    private void OnEnable()
    {
        _detector.TargetChanged += OnTargetChanged;
        _eventReceiver.HitEvent += OnHitEvent;
        _eventReceiver.AttackFinishedEvent += OnAttackFinishedEvent;

        _currentTarget = _detector.CurrentTarget;
    }

    private void OnDisable()
    {
        if (_detector != null)
            _detector.TargetChanged -= OnTargetChanged;

        if (_eventReceiver != null)
        {
            _eventReceiver.HitEvent -= OnHitEvent;
            _eventReceiver.AttackFinishedEvent -= OnAttackFinishedEvent;
        }

        _currentTarget = null;
    }

    private void FixedUpdate()
    {
        TryStartAttack();
        TickMovementAndAnimation();
    }

    private void TryStartAttack()
    {
        if (_isAttacking)
            return;

        if (_currentTarget == null)
            return;

        if (_attackRangeChecker.IsTargetInRange(transform, _currentTarget) == false)
            return;

        if (_attack.TryStartAttack() == false)
            return;

        _isAttacking = true;
        _animator.PlayAttack();
        AttackStarted?.Invoke();
    }

    private void TickMovementAndAnimation()
    {
        if (_isAttacking)
        {
            StopMovement();
            ApplyFacingAndSpeed(0f);
            return;
        }

        if (_currentTarget == null)
        {
            TickPatrol();
            return;
        }

        TickChase();
    }

    private void TickPatrol()
    {
        _patrolStrategy.Tick();
        ApplyFacingAndSpeed(_patroller.CurrentSpeedX);
    }

    private void TickChase()
    {
        _patroller.Stop();
        _chaser.TickChase(_currentTarget);
        ApplyFacingAndSpeed(_chaser.CurrentSpeedX);
    }

    private void StopMovement()
    {
        _patroller.Stop();
        _chaser.Stop();
    }

    private void ApplyFacingAndSpeed(float speedX)
    {
        _directionFlipper.SetFacingDirection(speedX);
        _pointsMirror.SetFacingDirection(speedX);
        _animator.SetSpeed(Mathf.Abs(speedX));
    }

    private void OnTargetChanged(Transform target)
    {
        _currentTarget = target;
    }

    private void OnHitEvent()
    {
        _attack.Hit();
    }

    private void OnAttackFinishedEvent()
    {
        _attack.AttackFinished();
        _isAttacking = false;
    }
}