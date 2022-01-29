using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private AudioSource _punchSound;
    [SerializeField] private AudioSource _foodSound;
    [SerializeField] private float _minLength;
    [SerializeField] private int _scoreForHealth;

    private ParticleSystem _particleSystem;
    private Coroutine _setScaleJob;
    private int _health;
    private int _score;
    private int _recordScore;
    private float _length;
    private int _recordHealth;
    private float _recordLength;
    private string _recordScoreText;
    private string _recordHealthext;
    private string _recordLengthText;

    public int RecordScore => _recordScore;
    public int RecordHealth => _recordHealth;
    public float RecordLength => _recordLength;
    public float MinLength => _minLength;

    public event UnityAction<int> ChangedScore;
    public event UnityAction<int> ChangedHealth;
    public event UnityAction Died;

    private void Start()
    {
        _recordScoreText = "RecordScore";
        _recordHealthext = "RecordHealth";
        _recordLengthText = "RecordLength";

        _particleSystem = GetComponent<ParticleSystem>();
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (_length > _minLength)
        {
            _length -= (_length / 20) * Time.deltaTime;
            ChangedScore?.Invoke(_score);
        }
        RaycastHit[] hits = Physics.RaycastAll(_raycastPoint.position, Vector3.forward, _raycastDistance);
        foreach (var hit in hits)
        {
            Block block;
            if (hit.collider.TryGetComponent<Block>(out block))
            {
                block.StartParticleSystem();
            }
        }
    }

    public void StartGame()
    {
        _score = 0;
        _recordScore = PlayerPrefs.GetInt(_recordScoreText);
        _recordHealth = PlayerPrefs.GetInt(_recordHealthext);
        _recordLength = PlayerPrefs.GetFloat(_recordLengthText);
        _health = _startHealth;
        _length = _minLength;

        StartChangeScale();
        ChangedScore?.Invoke(_score);
        ChangedHealth?.Invoke(_health);
        Time.timeScale = 1;
    }

    private void AddLength(float nutritionalValue)
    {
        _length += nutritionalValue;
        StartChangeScale();
        if (_length > _recordLength)
        {
            _recordLength = _length;
            PlayerPrefs.SetFloat(_recordLengthText, _recordLength);
        }
    }

    private void AddScore()
    {
        _foodSound.Play();
        _score++;
        if (_score > _recordScore)
        {
            _recordScore = _score;
            PlayerPrefs.SetInt(_recordScoreText, _recordScore);
        }
        if (_score % _scoreForHealth == 0)
        {
            _health++;
            ChangedHealth?.Invoke(_health);
            if (_health > _recordHealth)
            {
                _recordHealth = _health;
                PlayerPrefs.SetInt(_recordHealthext, _recordHealth);
            }
        }
        ChangedScore?.Invoke(_score);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Food>(out Food food))
        {
            AddLength(food.NutritionalValue);
            AddScore();
        }
        if (other.TryGetComponent<Block>(out Block block))
        {
            TakeDamage(block.Damage);
            Destroy(other.gameObject);
        }
        other.gameObject.SetActive(false);
    }

    private void TakeDamage(int damage)
    {
        _punchSound.Play();
        _particleSystem.Play();
        _health -= damage;
        ChangedHealth?.Invoke(_health);
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
        Time.timeScale = 0;
    }

    private void StartChangeScale()
    {
        if (_setScaleJob != null)
            StopCoroutine(_setScaleJob);
        _setScaleJob = StartCoroutine(ChangeScale());
    }

    private IEnumerator ChangeScale()
    {
        Vector3 vector = transform.localScale;
        while (vector.x != _length)
        {
            vector.x = Mathf.MoveTowards(vector.x, _length, Time.deltaTime);
            vector.y = Mathf.MoveTowards(vector.y, _length, Time.deltaTime);
            vector.z = Mathf.MoveTowards(vector.z, _length, Time.deltaTime);
            transform.localScale = vector;
            yield return null;
        }
    }
}