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
    private IEnumerator _pathDrawCoroutine;

    private void OnValidate()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private IEnumerator PathCalculate()
    {
        while(true)
        {
            _currentTouchPosition = Input.mousePosition;
            Vector3 direction = ScreenToWorldCalculate(_startTouchPosition, _currentTouchPosition);
            if (direction.magnitude > GameManager.instance.GameSettings.TouchError) _playerController.DrawPathLine(direction);
            yield return null;
        }
    }

    public void TouchScreen(InputAction.CallbackContext context)
    {
        if (_playerController.GetVelocity() < GameManager.instance.GameSettings.MinSpeedToStartAcceleration)
        {
            if (context.started)
            {
                _startTouchPosition = Input.mousePosition;
                Time.timeScale = GameManager.instance.GameSettings.TouchTimeScale;
                _pathDrawCoroutine = PathCalculate();
                StartCoroutine(_pathDrawCoroutine);
            }

            if (context.canceled)
            {
                _endTouchPosition = Input.mousePosition;
                Vector3 direction = ScreenToWorldCalculate(_startTouchPosition, _endTouchPosition);
                if (direction.magnitude > GameManager.instance.GameSettings.TouchError) _playerController.AccelerateBall(direction);
                Time.timeScale = 1;
                _playerController.DrawPathLine(Vector3.zero);
                StopCoroutine(_pathDrawCoroutine);
            }
        }   
    }

    public Vector3 ScreenToWorldCalculate(Vector3 startPosition, Vector3 endPosition)
    {
        Camera mainCamera = Camera.main;
        float nearPlane = mainCamera.nearClipPlane;
        Vector3 startDot = mainCamera.ScreenToWorldPoint(new Vector3(startPosition.x, startPosition.y, nearPlane));
        Vector3 endDot = mainCamera.ScreenToWorldPoint(new Vector3(endPosition.x, endPosition.y, nearPlane));
        Vector3 direction = endDot - startDot;
        return direction;
    }
}
