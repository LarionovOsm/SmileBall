using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _finishPosition;

    #region References
    public Transform StartPosition => _startPosition;   
    public Transform FinishPosition => _finishPosition;
    #endregion

    #region CustomeEventFunctions
    public void StartLevel()
    {
        StartCoroutine(CheckLevelComplete());
    }
    #endregion

    #region LevelLogic
    private IEnumerator CheckLevelComplete()
    {
        while (true)
        {
            if (CheckPosition(GameManager.instance.PlayerController.gameObject.transform.position))
            {
                GameManager.instance.WinGame();
                StopAllCoroutines();
            }
            yield return null;
        }
    }

    private bool CheckPosition(Vector3 playerPosition)
    {
        float distance = (playerPosition - _finishPosition.position).magnitude;
        if (distance <= GameManager.instance.GameSettings.CheckPointDistanceError) return true;
        else return false;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_startPosition.position, 3f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_finishPosition.position, 3f);
    }
}
