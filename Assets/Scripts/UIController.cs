using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;
    
    public CanvasGroup IterationPanel;
    public GameObject GameplayPanel;
    public float TimeShowIterationPanel;
    public float TimeShowAndHideInteractionPanen = 1f;
    
    public GameObject WarningPanel;
    public Button WarningContinueButton;
    public Button WarningBwButton;
    public Volume Volume;

    private void Start()
    {
        MenuPanel.SetActive(false);
        WarningPanel.SetActive(true);
        IterationPanel.gameObject.SetActive(true);
        IterationPanel.DOFade(0, 0);
        GameplayPanel.SetActive(false);
        
        WarningContinueButton.onClick.AddListener(() => {
            WarningPanel.SetActive(false);
            MenuPanel.SetActive(true);
        });
        WarningBwButton.onClick.AddListener(() => {
            Debug.Log("Меняем тему");
            ColorAdjustments colorAdjustments;
            if(Volume.profile.TryGet<ColorAdjustments>( out colorAdjustments ) )
            {
                colorAdjustments.active = true;
            }
            WarningPanel.SetActive(false);
            MenuPanel.SetActive(true);
        });
    }

    public IEnumerator ShowIterationPanel()
    {
        IterationPanel.DOFade(1, TimeShowAndHideInteractionPanen);
        yield return new WaitForSeconds(TimeShowIterationPanel + TimeShowAndHideInteractionPanen);
        IterationPanel.DOFade(0, TimeShowAndHideInteractionPanen);
    }
}
