using UnityEngine;
using System.Collections.Generic;

public class SpaceShipManager : MonoBehaviour
{
    private NewIterationController iterationController = null;
    private void Start()
    {
        iterationController = (NewIterationController)FindObjectOfType(typeof(NewIterationController));
    }
    public void processPlayerSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement) {
        SpaceShipActionsReplay theReplayOfDeadPlayer = inDeadSpaceShipMovement.GetComponent<SpaceShipPlayerController>().stopRecordingAndGetReplay();
        _replayForClones.Add(theReplayOfDeadPlayer);

        _gameplayManager.EndIteration();
        // inDeadSpaceShipMovement.gameObject.SetActive(false);
    }

    public void processCloneSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement, bool inDeathCausedByPlayer) {
        if (inDeathCausedByPlayer) {
            var theReplayController = inDeadSpaceShipMovement.GetComponent<SpaceShipActionsReplayController>();
            _replayForClones.Remove(theReplayController.replay);
        }
        iterationController.activeClonesOnScene.Remove(inDeadSpaceShipMovement.gameObject);
        Destroy(inDeadSpaceShipMovement.gameObject);
    }

    public System.Collections.IEnumerator startSpawnClones(Vector3 inCloneSpawnPoint) {
        List<SpaceShipActionsReplay> theCurrentIteratorClones = new List<SpaceShipActionsReplay>(_replayForClones);

        int theCurrentSpawnIndex = 0;
        
        while (theCurrentSpawnIndex < theCurrentIteratorClones.Count) {
            var theCloneController = Instantiate(_clonePrefab);
            iterationController.activeClonesOnScene.Add(theCloneController.gameObject);
            theCloneController.transform.position = inCloneSpawnPoint;
            theCloneController.startReplayPlaying(_replayForClones[theCurrentSpawnIndex]);
            
            ++theCurrentSpawnIndex;
            
            yield return new WaitForSeconds(1f);
        }
    }

    [SerializeField]
    private SpaceShipActionsReplayController _clonePrefab;

    [SerializeField]
    private GameplayManager _gameplayManager;

    private List<SpaceShipActionsReplay> _replayForClones = new List<SpaceShipActionsReplay>();
}
