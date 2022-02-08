using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int _valueForHealth;

    private PlayerHealth _playerHealth;
    private int _value;

    public event UnityAction<int> Changed;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void Reset()
    {
        _value = 0;
        Changed?.Invoke(_value);
    }

    public void Add()
    {
        _value++;
        Changed?.Invoke(_value);
        if (_value % _valueForHealth == 0)
        {
            _playerHealth.Add();
        }
    }
}
