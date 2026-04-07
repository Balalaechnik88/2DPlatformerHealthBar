using UnityEngine;

public class AttackRangeChecker
{
    private const float MinDistanceEpsilon = 0.0001f;

    private readonly float _attackStartDistanceSquared;

    public AttackRangeChecker(float attackStartDistance)
    {
        _attackStartDistanceSquared = attackStartDistance * attackStartDistance;
    }

    public bool IsTargetInRange(Transform origin, Transform target)
    {
        if (origin == null || target == null)
            return false;

        Vector2 deltaToTarget = (Vector2)target.position - (Vector2)origin.position;
        float distanceSquared = deltaToTarget.sqrMagnitude;

        if (distanceSquared < MinDistanceEpsilon)
            return false;

        return distanceSquared <= _attackStartDistanceSquared;
    }
}