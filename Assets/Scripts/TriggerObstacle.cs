using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObstacle : MonoBehaviour
{
    public SpawnController spawnController = null;
    private float distanceToParent;
    private void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            distanceToParent = transform.parent.position.z - other.gameObject.transform.position.z;
            spawnController.MoveObstacle(transform.parent.gameObject);
            spawnController.MoveObjectsWaitingTeleport();
        }
    }
}
