using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food))
        {
            _player.AddLength(food.NutritionalValue);
            _player.AddScore();
        }
        if (other.TryGetComponent<Block>(out Block block))
        {
            _player.TakeDamage(block.Damage);
            Destroy(other.gameObject);
        }
        other.gameObject.SetActive(false);
    }
}
