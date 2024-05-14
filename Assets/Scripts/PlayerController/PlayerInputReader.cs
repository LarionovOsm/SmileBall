using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Vector3 _startTouchPosition;
    private Vector3 _endTouchPosition;
    private Vector3 _currentTouchPosition;
    private Vector3 _worldPosition;

    private void OnValidate()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (_playerController.GetVelocity() < GameManager.instance.GameSettings.MinSpeedToStartAcceleration)
        {
            if (context.started)
            {
                _startTouchPosition = Input.mousePosition;
                Time.timeScale = GameManager.instance.GameSettings.TouchTimeScale;
            }

            if (context.performed)
            {
                _currentTouchPosition = Input.mousePosition;
                Debug.Log(_currentTouchPosition);
                Vector3 direction = (_currentTouchPosition - _startTouchPosition);
                if (direction.magnitude > GameManager.instance.GameSettings.TouchError) _playerController.DrawPathLine(direction);
            }

            if (context.canceled)
            {
                _endTouchPosition = Input.mousePosition;
                Vector3 direction = (_endTouchPosition - _startTouchPosition);
                if (direction.magnitude > GameManager.instance.GameSettings.TouchError) _playerController.AccelerateBall(direction.normalized);
                Time.timeScale = 1;
            }
        }   
    }
}
