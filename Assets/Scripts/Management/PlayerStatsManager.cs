using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    #region References
    public int HealthAmount => _healthAmount;
    #endregion

    #region PlayerStatsFunctions
    public void ChangeHealth(int amount)
    {
        _healthAmount += amount;
        GameManager.instance.UIManager.RefreshHUD();
        if (_healthAmount <= 0) OnDie();
    }

    private void OnDie() 
    {
        GameManager.instance.PlayerController.ControlAccess(false);
        GameManager.instance.UIManager.ShowWinLosePopUp("Lose");
    }

    public void ResetStats()
    {
        _healthAmount = GameManager.instance.GameSettings.MaxHealthAmount;
    }
    #endregion
}
