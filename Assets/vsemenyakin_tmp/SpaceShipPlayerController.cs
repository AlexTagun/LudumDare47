using UnityEngine;

namespace GameInput
{
    public struct PushButton
    {
        public PushButton(KeyCode inKeyCode, System.Action inKeyPressedAction) {
            _keyCode = inKeyCode;
            _keyPressedAction = inKeyPressedAction;
            _keyWasPressed = false;
        }

        public void update() {
            bool theIsKeyPressed = Input.GetKey(_keyCode);
            if (theIsKeyPressed && !_keyWasPressed)
                _keyPressedAction();
            _keyWasPressed = theIsKeyPressed;
        }

        private KeyCode _keyCode;
        private System.Action _keyPressedAction;
        private bool _keyWasPressed;
    }
}

public class SpaceShipPlayerController : MonoBehaviour
{
    private void Awake() {
        _moveRightPushButton = new GameInput.PushButton(KeyCode.D, () => _movement.moveRight());
        _moveLeftPushButton = new GameInput.PushButton(KeyCode.A, () => _movement.moveLeft());
        _moveUpPushButton = new GameInput.PushButton(KeyCode.W, () => _movement.moveUp());
        _moveDownPushButton = new GameInput.PushButton(KeyCode.S, () => _movement.moveDown());
    }

    private void FixedUpdate() {
        _moveRightPushButton.update();
        _moveLeftPushButton.update();
        _moveUpPushButton.update();
        _moveDownPushButton.update();
    }

    [SerializeField]
    private SpaceShipMovement _movement = null;

    private GameInput.PushButton _moveRightPushButton;
    private GameInput.PushButton _moveLeftPushButton;
    private GameInput.PushButton _moveUpPushButton;
    private GameInput.PushButton _moveDownPushButton;
}
