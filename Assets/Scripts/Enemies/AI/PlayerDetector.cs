using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private LayerMask _playerLayerMask;

    public Transform FindPlayer()
    {
        return Physics2D
            .OverlapCircle(transform.position, _viewRadius, _playerLayerMask)
            ?.transform;
    }
}
