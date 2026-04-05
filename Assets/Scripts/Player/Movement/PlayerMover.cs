using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GroundSensor _ground;
    [SerializeField] private PlayerHorizontalMover _horizontalMover;
    [SerializeField] private PlayerJumper _jumper;
    [SerializeField] private SpriteDirectionFlipper _directionFlipper;
    [SerializeField] private FacingPointsMirror _pointsMirror;

    public bool IsGrounded => _ground.IsGrounded;
    public float CurrentSpeedX => _horizontalMover.CurrentSpeedX;

    private void Awake()
    {
        if (_input == null ||
            _ground == null ||
            _horizontalMover == null ||
            _jumper == null ||
            _directionFlipper == null ||
            _pointsMirror == null)
        {
            Debug.LogError($"[{nameof(PlayerMover)}] Не назначены обязательные ссылки. Скрипт отключён.", this);
            enabled = false;
        }
    }

    private void Update()
    {
        float horizontalInput = _input.Horizontal;

        _directionFlipper.SetFacingDirection(horizontalInput);
        _pointsMirror.SetFacingDirection(horizontalInput);

        bool jumpPressed = _input.ConsumeJumpPressed();
        _jumper.TryJump(IsGrounded, jumpPressed);
    }

    private void FixedUpdate()
    {
        _horizontalMover.ApplyHorizontal(_input.Horizontal);
    }
}
