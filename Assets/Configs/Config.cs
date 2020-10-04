﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 1)]
public class Config : ScriptableObject {
    public int PointsPerSecond;
    public int PointsForDestroyingObstacle;
}