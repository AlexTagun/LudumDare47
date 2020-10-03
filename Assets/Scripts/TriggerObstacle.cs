using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    public SpawnController spawnController = null;
    private void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(spawnController.MovingObstacleCoroutine(transform.parent.gameObject));
        }
    }
}
