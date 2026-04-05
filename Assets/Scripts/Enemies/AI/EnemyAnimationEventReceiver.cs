using System;
using UnityEngine;

public class EnemyAnimationEventReceiver : MonoBehaviour
{
    public event Action HitEvent;
    public event Action AttackFinishedEvent;

    public void EnemyAttackFinishedEvent()
    {
        AttackFinishedEvent?.Invoke();
    }

    public void EnemyHitEvent()
    {
        HitEvent?.Invoke();
    }
}
