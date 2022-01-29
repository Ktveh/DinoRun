using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _nutritionalValue;

    public float NutritionalValue => _nutritionalValue;

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
