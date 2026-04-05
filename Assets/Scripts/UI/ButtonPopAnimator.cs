using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPopAnimator : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Scale")]
    [SerializeField] private float _hoverScale = 1.06f;
    [SerializeField] private float _pressedScale = 0.94f;
    [SerializeField] private float _speed = 14f;

    private RectTransform _rectTransform;
    private Vector3 _baseScale;
    private Vector3 _targetScale;

    private bool _isHover;
    private bool _isPressed;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
        _baseScale = transform.localScale;
        _targetScale = _baseScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.unscaledDeltaTime * _speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHover = true;
        UpdateTargetScale();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHover = false;
        _isPressed = false;
        UpdateTargetScale();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        UpdateTargetScale();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        UpdateTargetScale();
    }

    private void UpdateTargetScale()
    {
        if (_isPressed)
        {
            _targetScale = _baseScale * _pressedScale;
            return;
        }

        if (_isHover)
        {
            _targetScale = _baseScale * _hoverScale;
            return;
        }

        _targetScale = _baseScale;
    }
}
