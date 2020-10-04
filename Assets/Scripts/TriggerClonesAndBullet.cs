using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClonesAndBullet : MonoBehaviour
{
    public SpawnController spawnController = null;

    void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PortableObject>())
        {
            Debug.Log("Обнаружен объект для ТП");
            spawnController.AddInListObjectsWaitingTeleport(other.gameObject);
        }
    }
}
