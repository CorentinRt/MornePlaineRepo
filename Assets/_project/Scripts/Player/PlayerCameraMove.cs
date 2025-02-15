using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraMove : MonoBehaviour
{
    #region Fields

    [Header("Inputs")]
    [SerializeField] private InputActionReference _move;

    [Header("Camera")]
    [SerializeField] private Camera _camera;

    [Header("Move")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveMaxSpeed;
    [SerializeField] private float _moveAcceleration;
    private Vector3 _moveDir;

    #endregion

    private void Awake()
    {
        if (_move)
        {
            _move.action.started += UpdateMoveDir;
            _move.action.performed += UpdateMoveDir;
            _move.action.canceled += UpdateMoveDir;
        }
    }
    private void OnDestroy()
    {
        if (_move)
        {
            _move.action.started -= UpdateMoveDir;
            _move.action.performed -= UpdateMoveDir;
            _move.action.canceled -= UpdateMoveDir;
        }
    }

    private void Update()
    {
        HandleMove();
    }

    private void UpdateMoveDir(InputAction.CallbackContext ctx)
    {
        Vector2 tempInputMove = ctx.ReadValue<Vector2>();

        tempInputMove.Normalize();

        _moveDir = new Vector3(tempInputMove.x, 0f, tempInputMove.y);
    }
    private void HandleMove()
    {
        _rb.AddForce(_moveDir * Time.deltaTime * _moveAcceleration);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _moveMaxSpeed);
    }

}
