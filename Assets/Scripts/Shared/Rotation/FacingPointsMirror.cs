using UnityEngine;

public class FacingPointsMirror : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private Vector3[] _initialLocalPositions;

    private void Awake()
    {
        if (_points == null || _points.Length == 0)
        {
            Debug.LogError($"[{nameof(FacingPointsMirror)}] Не назначены точки. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        _initialLocalPositions = new Vector3[_points.Length];

        for (int index = 0; index < _points.Length; index++)
        {
            Transform point = _points[index];

            if (point == null)
            {
                Debug.LogError($"[{nameof(FacingPointsMirror)}] В массиве есть null-точка. Скрипт отключён.", this);
                enabled = false;
                return;
            }

            _initialLocalPositions[index] = point.localPosition;
        }
    }

    public void SetFacingDirection(float directionX)
    {
        if (enabled == false)
            return;

        if (Mathf.Abs(directionX) < 0.01f)
            return;

        bool faceRight = directionX > 0f;

        for (int index = 0; index < _points.Length; index++)
        {
            Vector3 localPosition = _initialLocalPositions[index];
            localPosition.x = faceRight ? Mathf.Abs(localPosition.x) : -Mathf.Abs(localPosition.x);
            _points[index].localPosition = localPosition;
        }
    }
}
