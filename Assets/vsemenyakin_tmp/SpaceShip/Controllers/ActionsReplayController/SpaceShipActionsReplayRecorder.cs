using UnityEngine;

public class SpaceShipActionsReplayRecorder : MonoBehaviour, ISpaceShipActionsScanner
{
    public void startNewRecording() {
        _currentRecordingReplay = new SpaceShipActionsReplay();
        _recordingStartTime = Time.fixedTime;
    }

    public SpaceShipActionsReplay stopRecordingAndGetReplay() {
        SpaceShipActionsReplay theResult = _currentRecordingReplay;
        _currentRecordingReplay = null;
        return theResult;
    }

    #region ISpaceShipActionsScanner implementation
    public void scanMoveRight() {
        _currentRecordingReplay?.addAction(currentRecordingTime, SpaceShipActionsReplay.ActionType.MoveRight);
    }

    public void scanMoveLeft() {
        _currentRecordingReplay?.addAction(currentRecordingTime, SpaceShipActionsReplay.ActionType.MoveLeft);
    }

    public void scanMoveUp() {
        _currentRecordingReplay?.addAction(currentRecordingTime, SpaceShipActionsReplay.ActionType.MoveUp);
    }

    public void scanMoveDown() {
        _currentRecordingReplay?.addAction(currentRecordingTime, SpaceShipActionsReplay.ActionType.MoveDown);
    }

    public void scanSpawnRocket() {
        _currentRecordingReplay?.addAction(currentRecordingTime, SpaceShipActionsReplay.ActionType.SpawnRocket);
    }
#endregion

    private void Awake() {
        startNewRecording();
    }
    
    private float currentRecordingTime => Time.fixedTime - _recordingStartTime;

    //Fields
    private float _recordingStartTime = 0f;
    private SpaceShipActionsReplay _currentRecordingReplay = null;
}
