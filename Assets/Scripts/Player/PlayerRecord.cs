using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerLength))]
public class PlayerRecord : MonoBehaviour
{
    private PlayerLength _playerLength;
    private PlayerHealth _playerHealth;
    private PlayerScore _playerScore;

    private string _recordHealthText;
    private string _recordScoreText;
    private string _recordLengthText;

    private int _recordHealth;
    private int _recordScore;
    private float _recordLength;

    public int RecordHealth => _recordHealth;
    public int RecordScore => _recordScore;
    public float RecordLength => _recordLength;

    private void OnEnable()
    {
        _playerLength.Changed += CheckLengthRecord;
        _playerHealth.Changed += CheckHelthRecord;
        _playerScore.Changed += CheckScoreRecord;
    }

    private void OnDisable()
    {
        _playerLength.Changed -= CheckLengthRecord;
        _playerHealth.Changed -= CheckHelthRecord;
        _playerScore.Changed -= CheckScoreRecord;
    }

    private void Awake()
    {
        _playerLength = GetComponent<PlayerLength>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerScore = GetComponent<PlayerScore>();

        _recordScoreText = "RecordScore";
        _recordHealthText = "RecordHealth";
        _recordLengthText = "RecordLength";

        _recordScore = PlayerPrefs.GetInt(_recordScoreText);
        _recordHealth = PlayerPrefs.GetInt(_recordHealthText);
        _recordLength = PlayerPrefs.GetFloat(_recordLengthText);
    }

    private void CheckScoreRecord(int score)
    {
        if (score > _recordScore)
            SetScoreRecord(score);
    }

    private void CheckHelthRecord(int health)
    {
        if (health > _recordHealth)
            SetHealthRecord(health);
    }

    private void CheckLengthRecord(float length)
    {
        if (length > _recordLength)
            SetLengthRecord(length);
    }

    private void SetScoreRecord(int score)
    {
        _recordScore = score;
        PlayerPrefs.SetInt(_recordScoreText, _recordScore);
    }

    private void SetHealthRecord(int health)
    {
        _recordHealth = health;
        PlayerPrefs.SetInt(_recordHealthText, _recordHealth);
    }

    private void SetLengthRecord(float length)
    {
        _recordLength = length;
        PlayerPrefs.SetFloat(_recordLengthText, _recordLength);
    }
}

