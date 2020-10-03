using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStart : MonoBehaviour
{
    private Button buttonStart = null;
    private SpawnController spawnController = null;
    private void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        buttonStart = gameObject.GetComponent<Button>();
        buttonStart.onClick.AddListener(() => {
            Debug.Log("Click");
            spawnController.SpawnObstacle();
        });
    }

}
