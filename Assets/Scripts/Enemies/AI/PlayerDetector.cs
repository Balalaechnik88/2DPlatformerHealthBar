using System;
using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private float _searchIntervalSeconds = 0.2f;
    [SerializeField] private LayerMask _playerLayerMask;

    private Coroutine _searchRoutine;

    public event Action<Transform> TargetChanged;

    public Transform CurrentTarget { get; private set; }

    private void OnEnable()
    {
        _searchRoutine = StartCoroutine(SearchRoutine());
    }

    private void OnDisable()
    {
        if (_searchRoutine != null)
            StopCoroutine(_searchRoutine);

        _searchRoutine = null;
        CurrentTarget = null;
    }

    private IEnumerator SearchRoutine()
    {
        float searchIntervalSeconds = Mathf.Max(0.05f, _searchIntervalSeconds);
        WaitForSeconds wait = new WaitForSeconds(searchIntervalSeconds);

        while (true)
        {
            UpdateTarget();
            yield return wait;
        }
    }

    private void UpdateTarget()
    {
        if (CurrentTarget != null)
        {
            if (IsTargetInRange(CurrentTarget))
                return;

            SetTarget(null);
            return;
        }

        SetTarget(FindNearestPlayer());
    }

    private Transform FindNearestPlayer()
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _playerLayerMask);

        if (playerColliders == null || playerColliders.Length == 0)
            return null;

        Transform nearestTarget = null;

        float nearestDistanceSquared = float.MaxValue;

        for (int i = 0; i < playerColliders.Length; i++)
        {
            Collider2D playerCollider = playerColliders[i];

            if (playerCollider == null)
                continue;

            Vector2 deltaToPlayer = (Vector2)playerCollider.transform.position - (Vector2)transform.position;
            float distanceSquared = deltaToPlayer.sqrMagnitude;

            if (distanceSquared < nearestDistanceSquared)
            {
                nearestDistanceSquared = distanceSquared;
                nearestTarget = playerCollider.transform;
            }
        }

        return nearestTarget;
    }

    private bool IsTargetInRange(Transform target)
    {
        if (target == null)
            return false;

        Vector2 deltaToTarget = (Vector2)target.position - (Vector2)transform.position;
        float distanceSquared = deltaToTarget.sqrMagnitude;

        return distanceSquared <= _viewRadius * _viewRadius;
    }

    private void SetTarget(Transform target)
    {
        if (CurrentTarget == target)
            return;

        CurrentTarget = target;
        TargetChanged?.Invoke(CurrentTarget);
    }
}