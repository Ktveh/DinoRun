using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _hiScoreText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private PlayerRecord _playerRecord;

    private void Start()
    {
        SetRecordScoreInfo();
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _playerHealth.Changed += SetHealthInfo;
        _playerScore.Changed += SetScoreInfo;
        _player.Died += ShowGameOverInfo;
    }

    private void OnDisable()
    {
        _playerHealth.Changed -= SetHealthInfo;
        _playerScore.Changed -= SetScoreInfo;
        _player.Died -= ShowGameOverInfo;
    }

    public void HideTitleText()
    {
        _titleText.gameObject.SetActive(false);
        _hiScoreText.gameObject.SetActive(false);
    }

    public void ShowPauseInfo()
    {
        ShowTitleText("Pause");
    }

    private void ShowGameOverInfo()
    {
        ShowTitleText("GAME OVER");
    }

    private void ShowTitleText(string title)
    {
        SetRecordScoreInfo();
        _hiScoreText.gameObject.SetActive(true);
        _titleText.gameObject.SetActive(true);
        _titleText.text = title;
    } 

    private void SetHealthInfo(int health)
    {
        _healthText.text = "";
        for (int i = 0; i<health; i++)
            _healthText.text += "♥\n";
    }

    private void SetScoreInfo(int score)
    {
        _scoreText.text = $"{score}";
    }

    private void SetRecordScoreInfo()
    {
        _hiScoreText.text = $"Рекорд: {_playerRecord.RecordScore}";
    }
}
