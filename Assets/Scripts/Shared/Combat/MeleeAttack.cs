using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _attackPoint;

    [Header("Attack Settings")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackRadius = 0.8f;
    [SerializeField] private float _cooldownSeconds = 0.6f;
    [SerializeField] private LayerMask _targetLayerMask;

    [Header("Target Selection")]
    [SerializeField] private float _minTargetDistance = 0.01f;

    public bool IsAttacking => _isAttacking;
    public bool CanStartAttack => _isAttacking == false && _isOnCooldown == false;

    private bool _isAttacking;
    private bool _isOnCooldown;
    private Coroutine _cooldownRoutine;

    private void Awake()
    {
        if (_attackPoint == null)
        {
            Debug.LogError($"[{nameof(MeleeAttack)}] Не назначен AttackPoint. Скрипт отключён.", this);
            enabled = false;
        }
    }

    public void AttackFinished()
    {
        if (_isAttacking == false)
            return;

        _isAttacking = false;

        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);

        _cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    public void Hit()
    {
        if (_isAttacking == false)
            return;

        Vector2 attackOrigin = GetAttackOrigin();
        DamageReceiver nearestReceiver = FindNearestDamageReceiver(attackOrigin);

        if (nearestReceiver == null)
            return;

        Health targetHealth = nearestReceiver.Health;
        if (targetHealth == null)
            return;

        targetHealth.TakeDamage(_damage);
    }

    public bool TryStartAttack()
    {
        if (CanStartAttack == false)
            return false;

        _isAttacking = true;
        return true;
    }

    private IEnumerator CooldownRoutine()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(_cooldownSeconds);
        _isOnCooldown = false;
        _cooldownRoutine = null;
    }

    private Vector2 GetAttackOrigin()
    {
        return _attackPoint != null ? _attackPoint.position : transform.position;
    }

    private DamageReceiver FindNearestDamageReceiver(Vector2 attackOrigin)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin, _attackRadius, _targetLayerMask);

        if (hitColliders == null || hitColliders.Length == 0)
            return null;

        float minTargetDistanceSquared = _minTargetDistance * _minTargetDistance;

        DamageReceiver nearestReceiver = null;
        float nearestDistanceSquared = float.MaxValue;

        for (int colliderIndex = 0; colliderIndex < hitColliders.Length; colliderIndex++)
        {
            Collider2D hitCollider = hitColliders[colliderIndex];
            if (hitCollider == null)
                continue;

            if (hitCollider.TryGetComponent(out DamageReceiver damageReceiver) == false)
                continue;

            Health targetHealth = damageReceiver.Health;
            if (targetHealth == null)
                continue;

            Vector2 deltaToTarget = (Vector2)hitCollider.transform.position - attackOrigin;
            float distanceSquared = deltaToTarget.sqrMagnitude;

            if (distanceSquared < minTargetDistanceSquared)
                continue;

            if (distanceSquared < nearestDistanceSquared)
            {
                nearestDistanceSquared = distanceSquared;
                nearestReceiver = damageReceiver;
            }
        }

        return nearestReceiver;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 attackOrigin = Application.isPlaying
            ? GetAttackOrigin()
            : (_attackPoint != null ? (Vector2)_attackPoint.position : (Vector2)transform.position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackOrigin, _attackRadius);
    }
}
