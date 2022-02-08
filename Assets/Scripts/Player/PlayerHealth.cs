using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _startValue;

    private Player _player;
    private int _value;

    public event UnityAction<int> Changed;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public void Reset()
    {
        _value = _startValue;
        Changed?.Invoke(_value);
    }

    public void TakeDamage(int damage)
    {
        _value -= damage;
        Changed?.Invoke(_value);
        if (_value <= 0)
            _player.Die();
    }

    public void Add()
    {
        _value++;
        Changed?.Invoke(_value);
    }
}
