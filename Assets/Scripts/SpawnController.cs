using System.Collections;
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

    public Vector3 positionSpawnClone => new Vector3(0f, 0f, obstacles[obstacles.Count - 1].transform.position.z + StartDistanceFromPlayerToSpawnObstacles);
    // Start is called before the first frame update
    void Start()
    {
        SpawnPartsLevel();
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
        for (int i = 0; i < startSequenceObstacles.Count; i++)
        {
            obstacles[i].transform.position = position + new Vector3(0f, 0f, StartDistanceFromPlayerToSpawnObstacles + DistanceBetweenSpawnObstacles * i);
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
            var obstacle = Instantiate(obstaclePrefab, PlayerPrefab.transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnObstacles * i + StartDistanceFromPlayerToSpawnObstacles), Quaternion.identity);
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
        foreach (var item in objectsWaitingTeleport)
        {
            item.transform.position += new Vector3(0f, 0f, partsLevel.Count * DistanceBetweenSpawnPartLevel);
        }
    }

}
