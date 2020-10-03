using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    public int NumberObstacles = 10;
    public int NumberPartLevel = 50;
    public float DistanceBetweenSpawnObstacles;
    public float DistanceBetweenSpawnPartLevel;
    public float StartDistanceFromPlayerToSpawnObstacles = 50f;
    public GameObject obstaclePrefab;
    public GameObject partLevelPrefab;
    public int MaxNumberOfImmuneBlocks = 10;
    public int MinNumberOfImmuneBlocks = 0;
    [Header("Сколько времени должно пройти перед переносом препятствия вперед")]
    public float DelayBeforeMovingObstacle;
    [Header("Сколько времени должно пройти перед переносом трубки вперед")]
    public float DelayBeforeMovingPartLevel;

    public List<GameObject> partsLevel = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();

    public GameObject NextNearestObstacles => obstacles[0];
    // Start is called before the first frame update
    void Start()
    {
        SpawnPartsLevel();
    }


    public void SpawnObstacle()
    {
        if (obstacles.Count > 0)
        {
            obstacles.Clear();
        }
        for (int i = 0; i < NumberObstacles; i++)
        {
            var obstacle = Instantiate(obstaclePrefab, playerPrefab.transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnObstacles * i + StartDistanceFromPlayerToSpawnObstacles), Quaternion.identity);
            obstacles.Add(obstacle);
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

    public IEnumerator MovingObstacleCoroutine(GameObject obstacle) // не забыть выключить ее когда мы проиграли
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
    }

    private void MoveObstacle (GameObject obstacle)
    {
        obstacle.transform.position += new Vector3(0f, 0f, obstacles.Count * DistanceBetweenSpawnObstacles);
    }

    private void MovePartLevel(GameObject partLevel)
    {
        partLevel.transform.position += new Vector3(0f, 0f, partsLevel.Count * DistanceBetweenSpawnPartLevel);
    }
}
