using UnityEngine;
using System.Collections.Generic;

public class SpaceShipManager : MonoBehaviour
{
    public void processPlayerSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement) {
        SpaceShipActionsReplay theReplayOfDeadPlayer = inDeadSpaceShipMovement.GetComponent<SpaceShipPlayerController>().stopRecordingAndGetReplay();
        _replayForClones.Add(theReplayOfDeadPlayer);

        _gameplayManager.EndIteration();
        inDeadSpaceShipMovement.gameObject.SetActive(false);
    }

    public void processCloneSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement, bool inDeathCausedByPlayer) {
        if (inDeathCausedByPlayer) {
            var theReplayController = inDeadSpaceShipMovement.GetComponent<SpaceShipActionsReplayController>();
            _replayForClones.Remove(theReplayController.replay);
        }

        Destroy(inDeadSpaceShipMovement.gameObject);
    }

    public void spawnClones(Vector3 inCloneSpawnPoint) {
        if (_replayForClones.Count > 0) {
            var theCloneController = Instantiate(_clonePrefab);
            theCloneController.transform.position = inCloneSpawnPoint;
            theCloneController.startReplayPlaying(_replayForClones[0]);
        }
    }

    [SerializeField]
    private SpaceShipActionsReplayController _clonePrefab;

    [SerializeField]
    private GameplayManager _gameplayManager;

    private List<SpaceShipActionsReplay> _replayForClones = new List<SpaceShipActionsReplay>();
}
