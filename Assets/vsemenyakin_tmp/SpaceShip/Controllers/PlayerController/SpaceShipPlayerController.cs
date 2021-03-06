﻿using UnityEngine;

public class SpaceShipPlayerController : SpaceShipController
{
    public void startRecordingNewReplay() {
        _replayRecorder.startNewRecording();
    }

    public SpaceShipActionsReplay stopRecordingAndGetReplay() {
        return _replayRecorder.stopRecordingAndGetReplay();
    }

    private void Awake() {
        _pushButtons = new GameInput.PushButton[] {
            new GameInput.PushButton(KeyCode.D, moveRight),
            new GameInput.PushButton(KeyCode.A, moveLeft),
            new GameInput.PushButton(KeyCode.W, moveUp),
            new GameInput.PushButton(KeyCode.S, moveDown),
            new GameInput.PushButton(KeyCode.Space, spawnRocket)
        };

        attachActionsScanner(_replayRecorder);
        _replayRecorder.startNewRecording();
    }

    private void FixedUpdate() {
        foreach (GameInput.PushButton thePushButton in _pushButtons)
            thePushButton.update();
    }

    private void Update() {
        if (SwipeInput.swipedDown) moveDown();
        if (SwipeInput.swipedLeft) moveLeft();
        if (SwipeInput.swipedRight) moveRight();
        if (SwipeInput.swipedUp) moveUp();
        if(SwipeInput.tap) spawnRocket();
#if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetMouseButtonDown(0)) spawnRocket();
#endif
        
    }

    private GameInput.PushButton[] _pushButtons = null;

    [SerializeField]
    private SpaceShipActionsReplayRecorder _replayRecorder = null;
}
