using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour {
    [SerializeField] private Config _scriptableObject;
    public static Config Data;

    private void Awake() {
        Data = _scriptableObject;
    }
}