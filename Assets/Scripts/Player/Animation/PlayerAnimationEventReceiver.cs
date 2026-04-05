using System;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public event Action HitEvent;
    public event Action AttackFinishedEvent;

    public void PlayerAttackFinishedEvent()
    {
        AttackFinishedEvent?.Invoke();
    }

    public void PlayerHitEvent()
    {
        HitEvent?.Invoke();
    }
}
