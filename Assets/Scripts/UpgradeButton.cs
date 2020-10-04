using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public string textWithUpgrade = "upgrade";
    public string textWithoutUpgrade = "next";
    [SerializeField] private NewIterationController iterationController = null;
    private UIController UIController = null;
    private Button button = null;

    private void Start()
    {
        UIController = (UIController)FindObjectOfType(typeof(UIController));
        iterationController = (NewIterationController)FindObjectOfType(typeof(NewIterationController));
        button.onClick.AddListener(() =>
        {
            iterationController.StartNewIteration();
            //UIController.панель закрыть
            
        });
    }


}
