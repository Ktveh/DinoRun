using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerScore))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerLength))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;
    private PlayerLength _playerLength;
    private PlayerHealth _playerHealth;
    private PlayerScore _playerScore;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerScore = GetComponent<PlayerScore>();
        _playerLength = GetComponent<PlayerLength>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food))
        {
            _player.ReactOnFood();
            _playerScore.Add();
            _playerLength.Add(food.NutritionalValue);
        }
        if (other.TryGetComponent<Block>(out Block block))
        {
            _player.ReactOnPunch();
            _playerHealth.TakeDamage(block.Damage);
        }
        other.gameObject.SetActive(false);
    }
}