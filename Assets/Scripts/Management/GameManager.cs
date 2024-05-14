using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("Managers")]
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerStatsManager _playerStatsManager;
    [Space]
    [SerializeField] private LevelController _levelController;
    [Space]
    [SerializeField] private PlayerController _playerController;
    [Space]
    [Header ("Projectile Generators")]
    [SerializeField] private ProjectileGenerator _cannonGenerator;
    [SerializeField] private ProjectileGenerator _laserGenerator;

    #region References
    public GameSettings GameSettings => _gameSettings;
    public UIManager UIManager => _uiManager;
    public PlayerStatsManager PlayerStatsManager => _playerStatsManager;
    public LevelController LevelController => _levelController; 
    public PlayerController PlayerController => _playerController;  
    public ProjectileGenerator CannonGenerator => _cannonGenerator;
    public ProjectileGenerator LaserGenerator => _laserGenerator;   
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        _uiManager.StartGame();
        _levelController.StartLevel();
        _playerController.ResetPlayer();
        _playerStatsManager.ResetStats();
    }

    public void WinGame()
    {
        _playerController.ControlAccess(false);
        _uiManager.ShowWinLosePopUp("Win");
    }
}
