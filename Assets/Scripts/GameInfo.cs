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
    [SerializeField] private TextMeshProUGUI _tasksText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Player _player;

    private int _taskOfScore;
    private int _taskOfLength;
    private int _taskOfHealth;

    private void Start()
    {
        _taskOfHealth = 5;
        _taskOfLength = 4;
        _taskOfScore = 500;

        ChangeRecordScoreInfo();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _audioSource.Play();
        _titleText.gameObject.SetActive(false);
        _hiScoreText.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _player.StartGame();
    }

    public void SetPause()
    {
        CheckTasks();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _hiScoreText.gameObject.SetActive(true);
            _tasksText.gameObject.SetActive(true);
            ChangeRecordScoreInfo();
        }
        else
        {
            Time.timeScale = 1;
            _hiScoreText.gameObject.SetActive(false);
            _tasksText.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _player.ChangedHealth += ChangeHealthInfo;
        _player.ChangedScore += ChangeScoreInfo;
        _player.Died += GameOver;
    }

    private void OnDisable()
    {
        _player.ChangedHealth -= ChangeHealthInfo;
        _player.ChangedScore -= ChangeScoreInfo;
        _player.Died -= GameOver;
    }

    private void CheckTasks()
    {
        _tasksText.text = "";
        WriteTask($"Собрать {_taskOfScore} очков\n", _player.RecordScore >= _taskOfScore);
        WriteTask($"Вырасти в {_taskOfLength} раза\n", _player.RecordLength / _player.MinLength >= _taskOfLength);
        WriteTask($"Накопить {_taskOfHealth} жизней\n", _player.RecordHealth >= _taskOfHealth);
        if (_tasksText.text != "")
            _tasksText.text = "Задачи:\n" + _tasksText.text;
    }

    private void WriteTask(string text, bool task)
    {
        if (!task)
            _tasksText.text += text;
    }

    private void ChangeHealthInfo(int health)
    {
        _healthText.text = "";
        for (int i = 0; i<health; i++)
            _healthText.text += "♥\n";
    }

    private void ChangeScoreInfo(int score)
    {
        _scoreText.text = $"{score}";
    }

    private void ChangeRecordScoreInfo()
    {
        _hiScoreText.text = $"Рекорд: {_player.RecordScore}";
    }

    private void GameOver()
    {
        ChangeRecordScoreInfo();
        _startButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _hiScoreText.gameObject.SetActive(true);
        _titleText.gameObject.SetActive(true);
        _titleText.text = "GAME OVER";
    } 
}
