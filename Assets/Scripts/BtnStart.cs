﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStart : MonoBehaviour
{
    private UIController UIController = null;
    private Button buttonStart = null;
    private SpawnController spawnController = null;
    [SerializeField] private GameplayManager _gameplayManager;
    private void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        UIController = (UIController)FindObjectOfType(typeof(UIController));
        buttonStart = gameObject.GetComponent<Button>();
        buttonStart.onClick.AddListener(() => {
            spawnController.SpawnObstacle();
            //UIController.StartCoroutine(UIController.ShowIterationPanel());
            UIController.GameplayPanel.SetActive(true);
            UIController.MenuPanel.SetActive(false);
            _gameplayManager.StartIteration();
        });
    }

}
