using UnityEngine;

namespace GameInput
{
    public class PushButton
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

        private KeyCode _keyCode = KeyCode.None;
        private System.Action _keyPressedAction = null;
        private bool _keyWasPressed = false;
    }
}