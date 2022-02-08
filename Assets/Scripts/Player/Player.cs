using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem), typeof(PlayerLength))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerScore))]
public class Player : MonoBehaviour
{
    [SerializeField] private Audio _audio;

    private PlayerHealth _health;
    private PlayerScore _score;
    private PlayerLength _length;
    private ParticleSystem _particleSystem;

    public event UnityAction Died;

    private void Start()
    {
        _health = GetComponent<PlayerHealth>();
        _score = GetComponent<PlayerScore>();
        _length = GetComponent<PlayerLength>();
        _particleSystem = GetComponent<ParticleSystem>();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _score.Reset();
        _health.Reset();
        _length.Reset();
        _audio.PlayLevelMusic();

        Time.timeScale = 1;
    }

    public void Die()
    {
        Died?.Invoke();
        Time.timeScale = 0;
    }

    public void ReactOnFood()
    {
        _audio.PlayFoodSound();
    }

    public void ReactOnPunch()
    {
        _audio.PlayPunchSound();
        _particleSystem.Play();
    }
}