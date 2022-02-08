using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TasksHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tasksText;
    [SerializeField] private PlayerRecord _playerRecord;
    [SerializeField] private PlayerLength _playerLength;

    private int _taskOfHealth;
    private int _taskOfScore;
    private int _taskOfLength;

    private void Start()
    {
        _taskOfHealth = 5;
        _taskOfLength = 4;
        _taskOfScore = 500;
    }

    public void ShowTasks()
    {
        CheckTasks();
        _tasksText.gameObject.SetActive(true);
    }

    public void HideTasks()
    {
        _tasksText.gameObject.SetActive(false);
    }

    private void CheckTasks()
    {
        _tasksText.text = "";
        WriteTask($"Собрать {_taskOfScore} очков\n", _playerRecord.RecordScore >= _taskOfScore);
        WriteTask($"Вырасти в {_taskOfLength} раза\n", _playerRecord.RecordLength / _playerLength.MinValue >= _taskOfLength);
        WriteTask($"Накопить {_taskOfHealth} жизней\n", _playerRecord.RecordHealth >= _taskOfHealth);
        if (_tasksText.text != "")
            _tasksText.text = "Задачи:\n" + _tasksText.text;
    }

    private void WriteTask(string text, bool task)
    {
        if (!task)
            _tasksText.text += text;
    }
}
