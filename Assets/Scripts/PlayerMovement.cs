using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _forwardAcceleration;
    [SerializeField] private float _backwardAcceleration;
    [SerializeField] private float _strafeAcceleration;
    [SerializeField] private float _gravityAcceleration;
    [SerializeField] private float _jumpAcceleration;
    [SerializeField] private float _maxForwardVelocity;
    [SerializeField] private float _maxBackwardVelocity;
    [SerializeField] private float _maxStrafeVelocity;
    [SerializeField] private float _maxFallVelocity;
    [SerializeField] private float _rotationVelocityFactor;
    [SerializeField] private float _maxHeadUpAngle;
    [SerializeField] private float _minHeadDownAngle;

    private CharacterController _controller;
    private Transform   _head;
    private Vector3     _acceleration;
    private Vector3     _velocity;
    private Vector3     _motion;
    private bool        _startJump;
    private float       _sinPI4;

    void Start()
    {
        _controller     = GetComponent<CharacterController>();
        _head           = GetComponentInChildren<Camera>().transform;
        _acceleration   = Vector3.zero;
        _velocity       = Vector3.zero;
        _motion         = Vector3.zero;
        _startJump      = false;
        _sinPI4         = Mathf.Sin(Mathf.PI / 4);

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
            _acceleration.z = _forwardAcceleration;
        else if (forwardAxis < 0f)
            _acceleration.z = _backwardAcceleration;
        else
            _acceleration.z = 0f;
    }

    private void UpdateStrafeAcceleration()
    {
        float strafeAxis = Input.GetAxis("Strafe");

        if (strafeAxis > 0f)
            _acceleration.x = _strafeAcceleration;
        else if (strafeAxis < 0f)
            _acceleration.x = -_strafeAcceleration;
        else
            _acceleration.x = 0f;
    }

    private void UpdateVerticalAcceleration()
    {
        if (_startJump)
            _acceleration.y = _jumpAcceleration;
        else
            _acceleration.y = _gravityAcceleration;
    }

    private void UpdateVelocity()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;

        if (_acceleration.z == 0f || _acceleration.z * _velocity.z < 0f)
            _velocity.z = 0f;
        else if (_velocity.x == 0f)
            _velocity.z = Mathf.Clamp(_velocity.z, _maxBackwardVelocity, _maxForwardVelocity);
        else
            _velocity.z = Mathf.Clamp(_velocity.z, _maxBackwardVelocity * _sinPI4, _maxForwardVelocity * _sinPI4);
       
        if (_acceleration.x == 0f || _acceleration.x * _velocity.x < 0f)
            _velocity.x = 0f;
        else if (_velocity.z == 0f)
            _velocity.x = Mathf.Clamp(_velocity.x, -_maxStrafeVelocity, _maxStrafeVelocity);
        else
            _velocity.x = Mathf.Clamp(_velocity.x, -_maxStrafeVelocity * _sinPI4, _maxStrafeVelocity * _sinPI4);

        if (_controller.isGrounded && !_startJump)
            _velocity.y = -0.1f;
        else
            _velocity.y = Mathf.Max(_velocity.y, _maxFallVelocity);

        _startJump = false;
    }

    private void UpdatePosition()
    {
        _motion = _velocity * Time.fixedDeltaTime;

        _motion = transform.TransformVector(_motion);

        _controller.Move(_motion);
    }

}
