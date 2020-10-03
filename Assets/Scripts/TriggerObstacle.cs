using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    public SpawnController spawnController = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnController.MovingObstacleCoroutine(transform.parent.gameObject);
        }
    }
}
