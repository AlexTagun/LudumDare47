﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarLoopObjectChecker : MonoBehaviour
{
    public SpawnController spawnController = null;

    void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        var distanseBetweenObstacles = spawnController.DistanceBetweenSpawnObstacles;
        gameObject.transform.localPosition = new Vector3(0f, 0f, -distanseBetweenObstacles * 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject triggeredObject = other.gameObject;
        if (triggeredObject.GetComponent<PortableObject>())
        {
            Debug.Log("Обнаружен объект для ТП ДАЛЕКО");
            spawnController.objectsWaitingTeleport.Remove(triggeredObject);
            var position = spawnController.obstacles[(spawnController.obstacles.Count - 2)].transform.position + new Vector3(0f, 0f, spawnController.distanceFromSecondTriggerToPlayer);
            triggeredObject.transform.position = position;


        }
    }
}
