using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayTestScript : MonoBehaviour
{
    private void Awake() {
        _playReplayPushButton = new GameInput.PushButton(KeyCode.Q, performTest);

        _startingPlayerSpaceShipPosition = _playerSpaceShip.transform.position;
    }

    private void FixedUpdate() {
        _playReplayPushButton.update();
    }

    private void performTest() {
        SpaceShipActionsReplay theReplay = destroyPlayerSavingReplay();
        spawnCloneThatPlaysReplay(theReplay);
    }

    private SpaceShipActionsReplay destroyPlayerSavingReplay() {
        SpaceShipActionsReplay theReplay = _playerSpaceShip.stopRecordingAndGetReplay();
        Destroy(_playerSpaceShip.gameObject);
        return theReplay;
    }

    private void spawnCloneThatPlaysReplay(SpaceShipActionsReplay inReplay) {
        var theCloneSpaceShip = Instantiate(_cloneSpaceShipPrefab);
        theCloneSpaceShip.transform.position = _startingPlayerSpaceShipPosition;
        theCloneSpaceShip.startReplayPlaying(inReplay);
    }

    [SerializeField]
    private SpaceShipPlayerController _playerSpaceShip = null;

    [SerializeField]
    private SpaceShipActionsReplayController _cloneSpaceShipPrefab = null;

    private Vector3 _startingPlayerSpaceShipPosition = Vector3.zero;
    private GameInput.PushButton _playReplayPushButton;
}
