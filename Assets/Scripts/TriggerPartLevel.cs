using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPartLevel : MonoBehaviour
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
            StartCoroutine(spawnController.MovingPartLevelCoroutine(transform.parent.gameObject));
        }
    }
}
