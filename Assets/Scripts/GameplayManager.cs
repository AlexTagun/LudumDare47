using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI iterationText;


    private bool _isGameplayState;

    private float _curPoints = 0;
    private float _curIterationIndex = 0;
    


    public void StartIteration() {
        _isGameplayState = true;
        _curIterationIndex++;
        UpdateIterationIndex();
    }
    
    public void EndIteration() {
        _isGameplayState = false;
    }

    private void Update() {
        if (!_isGameplayState) return;
        UpdateScore();
    }

    private void UpdateScore() {
        _curPoints += ConfigManager.Data.PointsPerSecond * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(_curPoints).ToString();
    }
    
    private void UpdateIterationIndex() {
        iterationText.text = $"Iteration: {_curIterationIndex}";
    }
}