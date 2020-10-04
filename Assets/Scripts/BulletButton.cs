using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletButton : MonoBehaviour
{
    private SpaceShipPlayerController shipPlayerController = null;
    private Button button = null;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        shipPlayerController = (SpaceShipPlayerController)FindObjectOfType(typeof(SpaceShipPlayerController));
        button.onClick.AddListener(() =>
        {
            shipPlayerController.RocketSpawner.spawnRocket();
        });
    }


}
