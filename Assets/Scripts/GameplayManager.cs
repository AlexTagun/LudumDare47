using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI iterationText;
    [SerializeField] private GameObject infoWindow;
    [SerializeField] private Button infoWindowNextButton;
    [SerializeField] private Button infoWindowUpgradeButton;
    [SerializeField] private TextMeshProUGUI infoWindowText;
    [SerializeField] private TextMeshProUGUI cloneInfoText;
    [SerializeField] private Image cloneInfoImage;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private NewIterationController iterationController;
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private TextMeshProUGUI winWindowInfoText;
    [SerializeField] private TextMeshProUGUI winWindowInfoTextSecond;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private Button winWindowButton;
    [SerializeField] private Color cloneCountingColor;
    [SerializeField] private Color timerColor;


    private bool _isGameplayState;

    private float _curPoints = 0;
    private float _totalPoints = 0;
    private float _curIterationIndex = 0;
    private int _curLevel = 0;
    private int _curBulletCount = 0;

    public int CurBulletCount {
        get => _curBulletCount;
        set {
            _curBulletCount = value; 
            bulletCountText.text = _curBulletCount.ToString();
        }
    }
    private void Awake() {
        infoWindowNextButton.onClick.AddListener(StartIteration);

        infoWindowUpgradeButton.onClick.AddListener((() => {
            _totalPoints -= ConfigManager.Data.LevelPointCost[_curLevel];
            _curLevel++;
            UpdateInfoWindow();
        }));
        winWindowButton.onClick.AddListener((() => {
            Application.Quit();
        }));
    }

    public void StartIteration() {
        _curPoints = 0;
        cloneInfoText.gameObject.transform.parent.gameObject.SetActive(false);
        _curBulletCount = ConfigManager.Data.StartBulletCount + _curLevel;
        bulletCountText.text = _curBulletCount.ToString();
        _isGameplayState = true;
        _curIterationIndex++;
        infoWindow.SetActive(false);
        // spaceShip.SetActive(true);
        UpdateIterationIndex();
        Time.timeScale = 1;
        SwipeInput.EnableTap = false;
        var temp = 1;
        DOTween.To(() => 1, x => temp = x, 1, 2).OnComplete(() => {
            SwipeInput.EnableTap = true;
        });
        if (_curIterationIndex != 1)
        {
            iterationController.StartNewIteration();
        }

        FindObjectOfType<SpaceShipPlayerController>().startRecordingNewReplay();
    }
    
    public void EndIteration(bool isWin) {
        if(_timerCoroutine != null) StopCoroutine(_timerCoroutine);
        _timerCoroutine = null;
        _totalPoints += _curPoints;
        _isGameplayState = false;
        
        if (iterationController.spawnCoroutine != null)
        {
            StopCoroutine(iterationController.spawnCoroutine);
        }

        if (isWin) {
            winWindowInfoText.text = $"YOU’VE UNSTUCKED FROM THE LOOP IN {_curIterationIndex} ITERATIONS.";
            winWindowInfoTextSecond.text = $"YOUR PLACE IN THE WORLD:\n{Random.Range(1,1000)}th";
            winWindow.gameObject.SetActive(true);
        } else {
            UpdateInfoWindow();
            infoWindow.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
    }

    private void Update() {
        if (!_isGameplayState) return;
        UpdateScore();

        var ships = GameObject.FindObjectsOfType<SpaceShipActionsReplayController>();
        if (ships == null || ships.Length == 0 && _timerCoroutine == null) {
            StartTimer();
            return;
        }

        if (ships.Length > 0) {
            cloneInfoText.gameObject.transform.parent.gameObject.SetActive(true);
            cloneInfoImage.color = cloneCountingColor;
            cloneInfoText.text = $"{ships.Length} CLONES LEFT";
        } else {
            // cloneInfoText.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void UpdateScore() {
        _curPoints += ConfigManager.Data.PointsPerSecond * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(_curPoints).ToString();
    }
    
    private void UpdateIterationIndex() {
        iterationText.text = $"Iteration: {_curIterationIndex}";
    }

    private void UpdateInfoWindow() {
        if (_curLevel + 1 > ConfigManager.Data.LevelPointCost.Length) {
            infoWindowUpgradeButton.gameObject.SetActive(false);
            infoWindowText.text = $"POINTS EARNED: {Mathf.RoundToInt(_curPoints).ToString()}\n" +
                                  $"TOTAL POINTS: {Mathf.RoundToInt(_totalPoints).ToString()}\n" +
                                  $"UPGRADE AVAILABLE: No";
            return;
        }
        var upgradeAvailable = _totalPoints >= ConfigManager.Data.LevelPointCost[_curLevel] ? "<color=#05D9E7>Yes</color>" : "<color=#FF2A6D>No</color>";
        infoWindowUpgradeButton.gameObject.SetActive(_totalPoints >= ConfigManager.Data.LevelPointCost[_curLevel]);
        infoWindowText.text = $"POINTS EARNED: {Mathf.RoundToInt(_curPoints).ToString()}\n" +
                              $"TOTAL POINTS: {Mathf.RoundToInt(_totalPoints).ToString()}\n" +
                              $"UPGRADE AVAILABLE: {upgradeAvailable}\n" +
                              $"NEXT UPGRADE: {ConfigManager.Data.LevelPointCost[_curLevel]}\n"/* +
                              $"CLONES DESTROYED: 0\n" +
                              $"CLONES LEFT: 0\n"*/;
    }

    private float _secondsLeft;
    private bool _isTimerStarts;
    private Coroutine _timerCoroutine;

    public void StartTimer() {
        _timerCoroutine = StartCoroutine(TimerCoroutine());
    }
    

    private IEnumerator TimerCoroutine() {
        _isTimerStarts = true;
        _secondsLeft = ConfigManager.Data.WinTimer;
        cloneInfoText.gameObject.transform.parent.gameObject.SetActive(true);
        cloneInfoImage.color = timerColor;
        cloneInfoText.text = $"{_secondsLeft}";
        while (_secondsLeft >= 1) {
            _secondsLeft -= 1;
            yield return new WaitForSeconds(1);
            cloneInfoText.text = $"{_secondsLeft}";
        }
        EndIteration(true);
        _isTimerStarts = false;
    }
}