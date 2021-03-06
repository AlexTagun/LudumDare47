﻿using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
	//For hack rocket spawn Button
    public RocketSpawner RocketSpawner => _rocketSpawner;

    public void attachActionsScanner(ISpaceShipActionsScanner inScanner) {
        _scanner = inScanner;
    }

    private void Start() {
        if (_movement.isInverted()) {
            _rocketSpawner.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _rocketSpawner.transform.localPosition = -_rocketSpawner.transform.localPosition;
        }
    }

    protected void moveRight() {
        _movement.moveRight();
        _scanner?.scanMoveRight();
    }

    protected void moveLeft() {
        _movement.moveLeft();
        _scanner?.scanMoveLeft();
    }

    protected void moveUp() {
        _movement.moveUp();
        _scanner?.scanMoveUp();
    }

    protected void moveDown() {
        _movement.moveDown();
        _scanner?.scanMoveDown();
    }

    protected void spawnRocket() {
        _rocketSpawner.spawnRocket(_movement);
        _scanner?.scanSpawnRocket();
    }

    [SerializeField]
    private SpaceShipMovement _movement = null;

    [SerializeField]
    private RocketSpawner _rocketSpawner = null;

    private ISpaceShipActionsScanner _scanner = null;
}
