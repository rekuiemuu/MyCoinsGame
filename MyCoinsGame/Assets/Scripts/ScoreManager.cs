using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    public static ScoreManager Instance;
    [SerializeField] private Text _scoreCount;
    float _timer = 0f;

    void Start()
    {
        _scoreCount.enabled = true;
        Instance = this;
        _timer = 3f;
    }

    public void addScore(int _count)
    {
        _score += _count;
        _scoreCount.text = "Score: " + (_score).ToString();

        _scoreCount.enabled = true;
        _timer = 3f;
    }

    void Update()
    {
        if (_scoreCount.enabled)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0f && _scoreCount.enabled)
        {
            _scoreCount.enabled = false;
        }
    }
}


