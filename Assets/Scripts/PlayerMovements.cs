using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _forwardAccel;
    [SerializeField] private float _backwardAccel;
    [SerializeField] private float _sideAccel;
    [SerializeField] private float _gravityAccel;
    [SerializeField] private float _jumpAccel;
    [SerializeField] private float _maxForwardVelo;
    [SerializeField] private float _maxBackwardVelo;
    [SerializeField] private float _maxSideVelo;
    [SerializeField] private float _maxFallVelo;
    [SerializeField] private float _rotationVelocityFactor;
    [SerializeField] private float _maxHeadUpAngle;
    [SerializeField] private float _minHeadDownAngle;

    private CharacterController _controller;
    private Transform _head;
    private Vector3 _acceleration;
    private Vector3 _velocity;
    private Vector3 _motion;
    private bool _startJump;
    private float _sinPI4;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _head = GetComponentInChildren<Camera>().transform;
        _acceleration = Vector3.zero;
        _velocity = Vector3.zero;
        _motion = Vector3.zero;
        _startJump = false;
        _sinPI4 = Mathf.Sin(Mathf.PI / 4);

        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateBodyRotation();
        UpdateHeadRotation();
        CheckForJump();
    }

    private void UpdateBodyRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * _rotationVelocityFactor;

        transform.Rotate(0f, rotation, 0f);
    }

    private void UpdateHeadRotation()
    {
        Vector3 rotation = _head.localEulerAngles;

        rotation.x -= Input.GetAxis("Mouse Y") * _rotationVelocityFactor;

        if (rotation.x < 180)
            rotation.x = Mathf.Min(rotation.x, _maxHeadUpAngle);
        else
            rotation.x = Mathf.Max(rotation.x, _minHeadDownAngle);

        _head.localEulerAngles = rotation;
    }

    private void CheckForJump()
    {
        if (Input.GetButtonDown("Jump") && _controller.isGrounded)
            _startJump = true;
    }

    void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();
    }

    private void UpdateAcceleration()
    {
        UpdateForwardAcceleration();
        UpdateStrafeAcceleration();
        UpdateVerticalAcceleration();
    }

    private void UpdateForwardAcceleration()
    {
        float forwardAxis = Input.GetAxis("Forward");

        if (forwardAxis > 0f)
            _acceleration.z = _forwardAccel;
        else if (forwardAxis < 0f)
            _acceleration.z = _backwardAccel;
        else
            _acceleration.z = 0f;
    }

    private void UpdateStrafeAcceleration()
    {
        float strafeAxis = Input.GetAxis("Side");

        if (strafeAxis > 0f)
            _acceleration.x = _sideAccel;
        else if (strafeAxis < 0f)
            _acceleration.x = -_sideAccel;
        else
            _acceleration.x = 0f;
    }

    private void UpdateVerticalAcceleration()
    {
        if (_startJump)
            _acceleration.y = _jumpAccel;
        else
            _acceleration.y = _gravityAccel;
    }

    private void UpdateVelocity()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;

        if (_acceleration.z == 0f || _acceleration.z * _velocity.z < 0f)
            _velocity.z = 0f;
        else if (_velocity.x == 0f)
            _velocity.z = Mathf.Clamp(_velocity.z, _maxBackwardVelo, _maxForwardVelo);
        else
            _velocity.z = Mathf.Clamp(_velocity.z, _maxBackwardVelo * _sinPI4, _maxForwardVelo * _sinPI4);

        if (_acceleration.x == 0f || _acceleration.x * _velocity.x < 0f)
            _velocity.x = 0f;
        else if (_velocity.z == 0f)
            _velocity.x = Mathf.Clamp(_velocity.x, -_maxSideVelo, _maxSideVelo);
        else
            _velocity.x = Mathf.Clamp(_velocity.x, -_maxSideVelo * _sinPI4, _maxSideVelo * _sinPI4);

        if (_controller.isGrounded && !_startJump)
            _velocity.y = -0.1f;
        else
            _velocity.y = Mathf.Max(_velocity.y, _maxFallVelo);

        _startJump = false;
    }

    private void UpdatePosition()
    {
        _motion = _velocity * Time.fixedDeltaTime;

        _motion = transform.TransformVector(_motion);

        _controller.Move(_motion);
    }

}
