using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 1)]
public class Config : ScriptableObject {
    
    
    public int PointsPerSecond;
    public int PointsForDestroyingObstacle;
    [Header("Speed")]
    public float StartShipSpeed;
    public float IntervalBetweenSpeedIncrease;
    public float PlusToSpeed;


    public int[] LevelPointCost;
    public int StartBulletCount;
    [Header("Level")]
    public int NumberObstacles = 10;
    public int NumberPartLevel = 50;
    public float DistanceBetweenSpawnObstacles;
    public float DistanceBetweenSpawnPartLevel;
    public float StartDistanceFromPlayerToSpawnObstacles = 50f;
    public int MaxNumberOfImmuneBlocks = 10;
    public int MinNumberOfImmuneBlocks = 0;
}
