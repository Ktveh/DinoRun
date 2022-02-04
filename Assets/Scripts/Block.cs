using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Block : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _randomHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;
    [SerializeField] private int _damage;

    private ParticleSystem _particleSystem;
    private bool _isNear;

    public int Damage => _damage;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        Reset();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    public void AlertOfDanger()
    {
        if (!_isNear)
        {
            _isNear = true;
            _particleSystem.Play();
        }
    }

    public void Reset()
    {
        _particleSystem.Stop();
        _isNear = false;
        SetRandomHeight();
    }

    private void SetRandomHeight()
    {
        if (_randomHeight)
        {
            float PositionY = Random.Range(_minHeight, _maxHeight);
            transform.position = new Vector3(transform.position.x, PositionY, transform.position.z);
        }
    }
}
