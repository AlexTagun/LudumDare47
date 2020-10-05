using UnityEngine;
using System.Collections.Generic;

public class SpaceShipManager : MonoBehaviour
{
    private NewIterationController iterationController = null;
    private SpawnController spawnController = null;

    private void Start()
    {
        iterationController = (NewIterationController)FindObjectOfType(typeof(NewIterationController));
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
    }
    public void processPlayerSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement) {
        SpaceShipActionsReplay theReplayOfDeadPlayer = inDeadSpaceShipMovement.GetComponent<SpaceShipPlayerController>().stopRecordingAndGetReplay();
        _replayForClones.Add(theReplayOfDeadPlayer);

        _gameplayManager.EndIteration(false);
        // inDeadSpaceShipMovement.gameObject.SetActive(false);
    }

    public void processCloneSpaceShipDeath(SpaceShipMovement inDeadSpaceShipMovement, bool inDeathCausedByPlayer) {
        if (inDeathCausedByPlayer) {
            var theReplayController = inDeadSpaceShipMovement.GetComponent<SpaceShipActionsReplayController>();
            _replayForClones.Remove(theReplayController.replay);
        }
        Destroy(inDeadSpaceShipMovement.gameObject);
    }

    public System.Collections.IEnumerator startSpawnClones() {
        List<SpaceShipActionsReplay> theCurrentIteratorClones = new List<SpaceShipActionsReplay>(_replayForClones);

        int theCurrentSpawnIndex = 0;
        
        while (theCurrentSpawnIndex < theCurrentIteratorClones.Count) {
            SpaceShipActionsReplay theReplay = _replayForClones[theCurrentSpawnIndex];

            if (null != theReplay) {
                var theCloneController = Instantiate(_clonePrefab);
                iterationController.activeClonesOnScene.Add(theCloneController.gameObject);
                theCloneController.transform.position = spawnController.positionSpawnClone;
                theCloneController.startReplayPlaying(theReplay);

                //performFirstSpawnedCloneTest(theCloneController, theCurrentSpawnIndex);
            }

            ++theCurrentSpawnIndex;
            
            yield return new WaitForSeconds(1f);
        }
    }

    private void performFirstSpawnedCloneTest(SpaceShipActionsReplayController theCloneController, int theSpawnedCloneIndex) {
        if (0 == theSpawnedCloneIndex) {
            var thePlayerController = FindObjectOfType<SpaceShipPlayerController>();
            thePlayerController.GetComponent<SpaceShipMovement>().makeHackStop();

            Camera theCamera = thePlayerController.GetComponentInChildren<Camera>();
            theCamera.transform.SetParent(theCloneController.transform, false);

            theCamera.transform.localPosition = new Vector3(
                theCamera.transform.localPosition.x,
                theCamera.transform.localPosition.y,
                -theCamera.transform.localPosition.z);
            theCamera.transform.rotation = Quaternion.Euler(
                theCamera.transform.rotation.x,
                180f,
                theCamera.transform.rotation.z);
        }
    }

    [SerializeField]
    private SpaceShipActionsReplayController _clonePrefab;

    [SerializeField]
    private GameplayManager _gameplayManager;

    private List<SpaceShipActionsReplay> _replayForClones = new List<SpaceShipActionsReplay>();
}
