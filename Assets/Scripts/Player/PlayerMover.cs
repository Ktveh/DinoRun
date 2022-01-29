using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stepSize;
    [SerializeField] private float _leftBoard;
    [SerializeField] private float _rightBoard;
    [SerializeField] private float _touchDeadzone;

    private PlayerInput _playerInput;
    private Vector3 _targetPosition;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _targetPosition = transform.position;
    }

    private void Start()
    {
        _playerInput.Player.MoveRight.performed += ctx => MoveRight();
        _playerInput.Player.MoveLeft.performed += ctx => MoveLeft();
        _playerInput.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _playerInput.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        _endTouchPosition = _playerInput.Touch.TouchPosition.ReadValue<Vector2>();
        if (Math.Abs(_endTouchPosition.x - _startTouchPosition.x) > _touchDeadzone)
        { 
        if (_endTouchPosition.x > _startTouchPosition.x)
            MoveRight();
        if (_endTouchPosition.x < _startTouchPosition.x)
            MoveLeft();
        }
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        _startTouchPosition = _playerInput.Touch.TouchPosition.ReadValue<Vector2>();
    }

    private void Update()
    {
        _targetPosition.y = transform.position.y;
        _targetPosition.z = transform.position.z;
        if (transform.position.x != _targetPosition.x)
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void MoveRight()
    {
        SetNextPosition(_stepSize);
    }

    private void MoveLeft()
    {
        SetNextPosition(-_stepSize);
    }

    private void SetNextPosition(float stepSize)
    {
        float targetX = _targetPosition.x + stepSize;
        if ((targetX > _leftBoard && targetX < _rightBoard))
            _targetPosition = new Vector3(targetX, _targetPosition.y, _targetPosition.z);
    }
}
