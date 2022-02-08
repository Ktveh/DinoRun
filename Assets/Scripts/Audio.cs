using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _levelMusic;
    [SerializeField] private AudioSource _punchSound;
    [SerializeField] private AudioSource _foodSound;

    public void PlayPunchSound()
    {
        _punchSound.Play();
    }

    public void PlayFoodSound()
    {
        _foodSound.Play();
    }

    public void PlayLevelMusic()
    {
        _levelMusic.Play();
    }
}
