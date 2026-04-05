using System.Collections;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private float _checkRadius = 0.15f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkIntervalSeconds = 0.1f;

    private Coroutine _checkRoutine;
    public bool IsGrounded { get; private set; }

    private void OnEnable()
    {
        _checkRoutine = StartCoroutine(CheckGroundRoutine());
    }

    private void OnDisable()
    {
        if (_checkRoutine != null)
            StopCoroutine(_checkRoutine);

        _checkRoutine = null;
        IsGrounded = false;
    }

    private IEnumerator CheckGroundRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkIntervalSeconds);

        while (true)
        {
            UpdateGroundedState();
            yield return wait;
        }
    }

    private void UpdateGroundedState()
    {
        if (_checkPoint == null)
        {
            IsGrounded = false;
            return;
        }

        IsGrounded = Physics2D.OverlapCircle(_checkPoint.position, _checkRadius, _groundMask);
    }
}
