using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointPatroller : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _reachDistanceX = 0.1f;

    private Rigidbody2D _rigidbody2D;
    private int _currentWaypointIndex;
    public float CurrentSpeedX { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Stop()
    {
        CurrentSpeedX = 0f;
        _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
    }

    public void TickPatrol()
    {
        if (_waypoints == null || _waypoints.Length < 2)
            return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        if (targetWaypoint == null)
            return;

        float deltaXToTarget = targetWaypoint.position.x - transform.position.x;

        if (Mathf.Abs(deltaXToTarget) <= _reachDistanceX)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Length)
                _currentWaypointIndex = 0;

            targetWaypoint = _waypoints[_currentWaypointIndex];
            if (targetWaypoint == null)
                return;

            deltaXToTarget = targetWaypoint.position.x - transform.position.x;
        }

        float directionX = Mathf.Sign(deltaXToTarget);
        CurrentSpeedX = directionX * _moveSpeed;

        _rigidbody2D.velocity = new Vector2(CurrentSpeedX, _rigidbody2D.velocity.y);
    }
}
