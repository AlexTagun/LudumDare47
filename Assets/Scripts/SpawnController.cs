﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject PlayerPrefab = null;
    public int NumberObstacles = 10;
    public int NumberPartLevel = 50;
    public float DistanceBetweenSpawnObstacles;
    public float DistanceBetweenSpawnPartLevel;
    public float StartDistanceFromPlayerToSpawnObstacles = 50f;
    public GameObject obstaclePrefab;
    public GameObject partLevelPrefab;
    public int MaxNumberOfImmuneBlocks = 10;
    public int MinNumberOfImmuneBlocks = 0;
    /*[Header("Сколько времени должно пройти перед переносом препятствия вперед")]
    //public float DelayBeforeMovingObstacle;
    [Header("Сколько времени должно пройти перед переносом трубки вперед")]
    //public float DelayBeforeMovingPartLevel;*/

    [Header("Не заполнять!")]
    public List<GameObject> partsLevel = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject NextNearestObstacles => obstacles[0];

    public List<GameObject> objectsWaitingTeleport = new List<GameObject>();
    public List<GameObject> startSequenceObstacles = new List<GameObject>();

    [SerializeField] private Transform firtsTriggerPlayer = null;
    public float distanceFromFirstTriggerToPlayer = 0f;
    [SerializeField] private Transform secondTriggerPlayer = null;
    public float distanceFromSecondTriggerToPlayer = 0f;
    public Vector3 positionSpawnClone => new Vector3(0f, 0f, obstacles[obstacles.Count - 1].transform.position.z + StartDistanceFromPlayerToSpawnObstacles);
    // Start is called before the first frame update
    void Start() {
        NumberObstacles = ConfigManager.Data.NumberObstacles;
        NumberPartLevel = ConfigManager.Data.NumberPartLevel;
        DistanceBetweenSpawnObstacles = ConfigManager.Data.DistanceBetweenSpawnObstacles;
        DistanceBetweenSpawnPartLevel = ConfigManager.Data.DistanceBetweenSpawnPartLevel;
        MaxNumberOfImmuneBlocks = ConfigManager.Data.MaxNumberOfImmuneBlocks;
        MinNumberOfImmuneBlocks = ConfigManager.Data.MinNumberOfImmuneBlocks;
        
        SpawnPartsLevel();

        distanceFromFirstTriggerToPlayer = PlayerPrefab.transform.position.z - firtsTriggerPlayer.position.z;
        distanceFromSecondTriggerToPlayer = PlayerPrefab.transform.position.z - secondTriggerPlayer.position.z;

    }
    public void AddInListObjectsWaitingTeleport(GameObject objectWaitingTeleport)
    {
        objectsWaitingTeleport.Add(objectWaitingTeleport);
    }
    public void RemoveFromListObjectsWaitingTeleport(GameObject objectWaitingTeleport)
    {
        objectsWaitingTeleport.Remove(objectWaitingTeleport);
    }

    public void ReplaceForNewIteration(Vector3 position)
    {
        for (int i = 0; i < partsLevel.Count; i++)
        {
            partsLevel[i].transform.position = position + new Vector3(0f, 0f, -10 + DistanceBetweenSpawnPartLevel * i);
        }
        PlayerPrefab.transform.position = position;
        PlayerPrefab.GetComponent<SpaceShipMovement>().TPlayer();
        PlayerPrefab.GetComponent<SpaceShipMovement>().SetStartPlayerSpeed();
        for (int i = 0; i < startSequenceObstacles.Count; i++)
        {
            startSequenceObstacles[i].transform.position = position + new Vector3(0f, 0f, StartDistanceFromPlayerToSpawnObstacles + DistanceBetweenSpawnObstacles * i);
            obstacles[i] = startSequenceObstacles[i];
        }

    }

    public void SpawnObstacle()
    {
        if (obstacles.Count > 0)
        {
            obstacles.Clear();
        }
        for (int i = 0; i < NumberObstacles; i++)
        {
            var obstacle = Instantiate(obstaclePrefab, transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnObstacles * i + StartDistanceFromPlayerToSpawnObstacles + PlayerPrefab.transform.position.z), Quaternion.identity);
            obstacles.Add(obstacle);
            startSequenceObstacles.Add(obstacle);
        }

    }
    public void SpawnPartsLevel()
    {
        if (partsLevel.Count > 0)
        {
            obstacles.Clear();
        }
        for (int i = 0; i < NumberPartLevel; i++)
        {
            var partLevel = Instantiate(partLevelPrefab, transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnPartLevel * i), Quaternion.identity);
            partsLevel.Add(partLevel);
        }
    }

    /*public IEnumerator MovingObstacleCoroutine(GameObject obstacle) // не забыть выключить ее когда мы проиграли
    {
        obstacles.RemoveAt(0);
        obstacles.Add(obstacle);
        yield return new WaitForSeconds(DelayBeforeMovingObstacle);

        MoveObstacle(obstacle);
    }

    public IEnumerator MovingPartLevelCoroutine(GameObject partLevel)
    {
        yield return new WaitForSeconds(DelayBeforeMovingPartLevel);

        MovePartLevel(partLevel);
    }*/

    public void MoveObstacle (GameObject obstacle)
    {
        if(obstacle.Equals(obstacles[1]))
        {
            var firstObstacles = obstacles[0];
            obstacles.RemoveAt(0);
            obstacles.Add(firstObstacles);
            firstObstacles.transform.position += new Vector3(0f, 0f, obstacles.Count * DistanceBetweenSpawnObstacles);
            var componentObstacle = firstObstacles.GetComponent<Obstacle>();
            if (componentObstacle)
                componentObstacle.CheckForDestructiveBlock();
        }
    }

    public void MovePartLevel(GameObject partLevel)
    {
        if (partLevel.Equals(partsLevel[1]))
        {
            var firstPartLevel = partsLevel[0];
            partsLevel.RemoveAt(0);
            partsLevel.Add(firstPartLevel);
            firstPartLevel.transform.position += new Vector3(0f, 0f, partsLevel.Count * DistanceBetweenSpawnPartLevel);
        }
    }

    public void MoveObjectsWaitingTeleport()
    {
        Debug.Log("Начало ТП клонов" + objectsWaitingTeleport.Count);
        foreach (var item in objectsWaitingTeleport)
        {
            Debug.Log("Тп клон");
            if (item != null)
            {
                item.transform.position += new Vector3(0f, 0f, partsLevel.Count * DistanceBetweenSpawnPartLevel + distanceFromFirstTriggerToPlayer);
            }
        }
        objectsWaitingTeleport.Clear();
        /*for (int i = 0; i < objectsWaitingTeleport.Count; i++)
        {
            if (objectsWaitingTeleport[i] != null)
            {
                objectsWaitingTeleport[i].transform.position += new Vector3(0f, 0f, partsLevel.Count * DistanceBetweenSpawnPartLevel);
                objectsWaitingTeleport.Remove(item);
            }
        }*/
    }

}
