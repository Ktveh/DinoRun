using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameInfo), typeof(TasksHandler))]
public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Player _player;

    private bool _isPause;
    private GameInfo _gameInfo;
    private TasksHandler _taskHandler;

    private void OnEnable()
    {
        _player.Died += Reset;
    }

    private void OnDisable()
    {
        _player.Died -= Reset;
    }

    private void Start()
    {
        _isPause = false;
        _gameInfo = GetComponent<GameInfo>();
        _taskHandler = GetComponent<TasksHandler>();
    }

    public void Reset()
    {
        _startButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        _gameInfo.HideTitleText();
        _startButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _player.StartGame();
    }

    public void OnPauseButtonClick()
    {
        if (_isPause)
            OffPause();
        else
            OnPause();
    }

    private void OnPause()
    {
        _isPause = true;
        Time.timeScale = 0;
        _gameInfo.ShowPauseInfo();
        _taskHandler.ShowTasks();
    }
    
    private void OffPause()
    {
        _isPause = false;
        Time.timeScale = 1;
        _gameInfo.HideTitleText();
        _taskHandler.HideTasks();
    }
}
