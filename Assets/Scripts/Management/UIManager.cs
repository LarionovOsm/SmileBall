using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Start PopUp")]
    [SerializeField] private CanvasGroup _startPopUp;
    [Header("WinLose PopUp")]
    [SerializeField] private CanvasGroup _winLosePopUp;
    [SerializeField] private CanvasGroup _restartButton;
    [SerializeField] private TMP_Text _messegeText;
    [Header("HUD")]
    [SerializeField] private RectTransform _hUD;
    [SerializeField] private TMP_Text _healthValueText;
    private Sequence _uiSequence;

    #region References
    public TMP_Text HealthValueText => _healthValueText;
    #endregion

    #region EventFunctions
    private void Awake()
    {
        RestartCanvas();
    }
    #endregion

    #region CustomeEventFunctions
    public void StartGame()
    {
        _uiSequence = DOTween.Sequence()
            .Append(_startPopUp.DOFade(0, GameManager.instance.GameSettings.CanvasGroupFadeTime))
            .AppendInterval(GameManager.instance.GameSettings.CanvasGroupFadeTime)
            .AppendCallback(() => _startPopUp.gameObject.SetActive(false))
            .AppendCallback(() => _startPopUp.alpha = 1f)
            .AppendCallback(() => _hUD.gameObject.SetActive(true))
            .AppendCallback(() => ResetHUD())
            .AppendCallback(() => GameManager.instance.PlayerController.ControlAccess(true));
    }
    #endregion

    #region WinLoseGameFunctions
    public void ShowWinLosePopUp(string stateName)
    {
        switch (stateName)
        {
            case ("Win"):
                _messegeText.text = "You win!";
                break;
            case ("Lose"):
                _messegeText.text = "You lose!";
                break;
        }

        _uiSequence = DOTween.Sequence()
            .AppendCallback(() => _hUD.gameObject.SetActive(false))
            .AppendCallback(() => _winLosePopUp.gameObject.SetActive(true))
            .Append(_winLosePopUp.DOFade(1, GameManager.instance.GameSettings.CanvasGroupFadeTime))
            .AppendInterval(GameManager.instance.GameSettings.CanvasGroupFadeTime)
            .AppendCallback(() => _restartButton.gameObject.SetActive(true))
            .AppendInterval(2f)
            .Append(_restartButton.DOFade(1, GameManager.instance.GameSettings.CanvasGroupFadeTime));
    }
    #endregion

    #region HUD
    public void RefreshHUD()
    {
        HealthValueText.text = $"Health: {GameManager.instance.PlayerStatsManager.HealthAmount}";
    }

    public void ResetHUD()
    {
        HealthValueText.text = $"Health: {GameManager.instance.GameSettings.MaxHealthAmount}";
    }
    #endregion

    #region RestartCanvas
    public void RestartCanvas()
    {
        _startPopUp.gameObject.SetActive(true);
        _startPopUp.alpha = 1.0f;
        _hUD.gameObject.SetActive(false);
        _winLosePopUp.gameObject.SetActive(false);
        _winLosePopUp.alpha = 0f;
        _restartButton.gameObject.SetActive(false);
        _restartButton.alpha = 0f;
    }
    #endregion
}
