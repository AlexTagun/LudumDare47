using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPartLevel : MonoBehaviour
{
    public SpawnController spawnController = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnController.MovingPartLevelCoroutine(transform.parent.gameObject);
        }
    }
}
