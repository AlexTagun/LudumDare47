using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI iterationText;
    [SerializeField] private GameObject infoWindow;
    [SerializeField] private Button infoWindowNextButton;
    [SerializeField] private TextMeshProUGUI infoWindowText;
    [SerializeField] private GameObject spaceShip;


    private bool _isGameplayState;

    private float _curPoints = 0;
    private float _totalPoints = 0;
    private float _curIterationIndex = 0;
    private int _curLevel = 1;

    private void Awake() {
        infoWindowNextButton.onClick.AddListener(StartIteration);
    }

    public void StartIteration() {
        _isGameplayState = true;
        _curIterationIndex++;
        infoWindow.SetActive(false);
        spaceShip.SetActive(true);
        UpdateIterationIndex();
        Time.timeScale = 1;
    }
    
    public void EndIteration() {
        _totalPoints += _curPoints;
        _isGameplayState = false;
        UpdateInfoWindow();
        infoWindow.gameObject.SetActive(true);
        
        Time.timeScale = 0;
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

    private void UpdateInfoWindow() {
        var upgradeAvailable = _totalPoints >= ConfigManager.Data.LevelPointCost[_curLevel] ? "Yes" : "No";
        infoWindowText.text = $"POINTS EARNED: {Mathf.RoundToInt(_curPoints).ToString()}\n" +
                              $"TOTAL POINTS: {Mathf.RoundToInt(_totalPoints).ToString()}\n" +
                              $"UPGRADE AVAILABLE: {upgradeAvailable}\n" +
                              $"NEXT UPGRADE: {ConfigManager.Data.LevelPointCost[_curLevel]}\n" +
                              $"CLONES DESTROYED: 0\n" +
                              $"CLONES LEFT: 0\n";
    }
}