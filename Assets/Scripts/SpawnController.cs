using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int NumberObstacles = 10;
    public int NumberPartLevel = 4;
    public float DistanceBetweenSpawnObstacles;
    public float DistanceBetweenSpawnPartLevel;
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
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if (obstacles.Count > 0)
        {
            obstacles.Clear();
        }
        if (partsLevel.Count > 0)
        {
            obstacles.Clear();
        }
        for (int i = 0; i < NumberPartLevel; i++)
        {
            var partLevel = Instantiate(partLevelPrefab, transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnPartLevel * i), Quaternion.identity);
            partsLevel.Add(partLevel);
        }
        for (int i = 1; i <= NumberObstacles; i++)
        {
            var obstacle = Instantiate(obstaclePrefab, transform.position + new Vector3(0f, 0f, DistanceBetweenSpawnObstacles * i), Quaternion.identity);
            obstacles.Add(obstacle);
        }
    }
    public IEnumerator MovingObstacleCoroutine(GameObject obstacle) // не забыть выключить ее когда мы проиграли
    {
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
        obstacle.transform.position += new Vector3(0f, 0f, obstacles.Count) * DistanceBetweenSpawnObstacles;
    }

    private void MovePartLevel(GameObject partLevel)
    {
        partLevel.transform.position += new Vector3(0f, 0f, partsLevel.Count) * DistanceBetweenSpawnPartLevel;
    }
}
