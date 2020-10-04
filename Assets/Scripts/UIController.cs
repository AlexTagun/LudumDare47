using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public GameObject MenuPanel;
    public CanvasGroup IterationPanel;
    public GameObject GameplayPanel;
    public float TimeShowIterationPanel;
    public float TimeShowAndHideInteractionPanen = 1f;
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        IterationPanel.gameObject.SetActive(true);
        IterationPanel.DOFade(0, 0);
        GameplayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowIterationPanel()
    {
        IterationPanel.DOFade(1, TimeShowAndHideInteractionPanen);
        yield return new WaitForSeconds(TimeShowIterationPanel + TimeShowAndHideInteractionPanen);
        IterationPanel.DOFade(0, TimeShowAndHideInteractionPanen);
    }
}
