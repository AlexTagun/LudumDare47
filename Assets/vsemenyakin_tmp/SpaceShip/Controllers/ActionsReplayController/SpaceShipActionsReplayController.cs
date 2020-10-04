using UnityEngine;

public class SpaceShipActionsReplayController : SpaceShipController
{
    public void startReplayPlaying(SpaceShipActionsReplay inReplay) {
        _playingStartTime = Time.fixedTime;
        _replay = inReplay;
        _replayEnumerator = inReplay.getEnumerator();
    }

    private void FixedUpdate() {
        _replayEnumerator.moveProcessingPassedActions(currentPlayingTime, performActionReplay);
    }

    private void performActionReplay(SpaceShipActionsReplay.Action inAction) {
        switch (inAction.type) {
            case SpaceShipActionsReplay.ActionType.MoveRight:      moveRight();     break;
            case SpaceShipActionsReplay.ActionType.MoveLeft:       moveLeft();      break;
            case SpaceShipActionsReplay.ActionType.MoveUp:         moveUp();        break;
            case SpaceShipActionsReplay.ActionType.MoveDown:       moveDown();      break;
            case SpaceShipActionsReplay.ActionType.SpawnRocket:    spawnRocket();   break;
        }
    }

    private float currentPlayingTime => Time.fixedTime - _playingStartTime;

    //Fields
    private float _playingStartTime = 0f;
    private SpaceShipActionsReplay _replay = null;
    private SpaceShipActionsReplay.Enumerator _replayEnumerator;
}
