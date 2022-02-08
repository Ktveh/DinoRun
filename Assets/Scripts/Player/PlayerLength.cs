using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLength : MonoBehaviour
{
    [SerializeField] private float _minValue;
    [SerializeField] private float _reductionFactor;

    private Coroutine _setScaleJob;
    private float _value;

    public float MinValue => _minValue;

    public event UnityAction<float> Changed;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (_value > _minValue)
            _value -= _value * _reductionFactor * Time.deltaTime;
    }

    public void Reset()
    {
        _value = _minValue;
        StartChangePlayerScale();
    }

    public void Add(float nutritionalValue)
    {
        _value += nutritionalValue;
        Changed?.Invoke(_value);
        StartChangePlayerScale();
    }

    private void StartChangePlayerScale()
    {
        if (_setScaleJob != null)
            StopCoroutine(_setScaleJob);
        _setScaleJob = StartCoroutine(ChangePlayerScale());
    }

    private IEnumerator ChangePlayerScale()
    {
        Vector3 vector = transform.localScale;
        while (vector.x != _value)
        {
            vector.x = Mathf.MoveTowards(vector.x, _value, Time.deltaTime);
            vector.y = Mathf.MoveTowards(vector.y, _value, Time.deltaTime);
            vector.z = Mathf.MoveTowards(vector.z, _value, Time.deltaTime);
            transform.localScale = vector;
            yield return null;
        }
    }
}
