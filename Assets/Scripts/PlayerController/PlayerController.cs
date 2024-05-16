using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInputReader))]
public class PlayerController : MonoBehaviour
{
    private float _maxAcceleleration;
    private float _clampLimit;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private LineRenderer _pathLine;

    #region EventFunctions
    private void Start()
    {
        ResetPlayer();
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        _playerInput = GetComponent<PlayerInput>();
        _pathLine = this.transform.GetChild(0).GetComponent<LineRenderer>();
    }
    #endregion

    #region ControlFunctions
    public void AccelerateBall(Vector3 direction)
    {
        Vector3 clampDirection = ClampDirection(direction, _clampLimit);
        float accelertion = clampDirection.magnitude / _clampLimit * _maxAcceleleration;
        _rigidbody.AddForce(direction * accelertion, ForceMode.Impulse);
    }

    public void ControlAccess(bool enabled)
    {
        _playerInput.enabled = enabled;
    }
    #endregion

    #region ResetFunctions
    public void ResetPlayer()
    {
        this.gameObject.transform.position = GameManager.instance.LevelController.StartPosition.position;
        this._rigidbody.velocity = Vector3.zero;
        _maxAcceleleration = GameManager.instance.GameSettings.MaxAcceleration;
        _clampLimit = GameManager.instance.GameSettings.ClampLimit;
        _playerInput.enabled = false;
    }
    #endregion

    #region FeaturesFunctions
    public void DrawPathLine(Vector3 direction) 
    {
        Vector3 clampDirection = ClampDirection(direction, _clampLimit);
        _pathLine.SetPosition(0, transform.position);
        _pathLine.SetPosition(1, clampDirection + transform.position);
    }

    public Vector3 ClampDirection(Vector3 direction, float clampLimit)
    {
        Vector3 clampDirection = new Vector3(Mathf.Clamp(direction.x, -clampLimit, clampLimit), Mathf.Clamp(direction.y, -clampLimit, clampLimit), direction.z);
        return clampDirection;
    }
    #endregion

    #region References
    public float GetVelocity()
    {
        return _rigidbody.velocity.magnitude;
    }
    #endregion
}
