using UnityEngine.InputSystem;

namespace Script.Camera
{
    public class CameraInput
    {
        private bool cameraLocked = true;
        private Keyboard keyboard;
        private InputSystem_Actions controls;

        public CameraInput()
        {
            keyboard = Keyboard.current;
            controls = new InputSystem_Actions();
            controls.Player.LockCamera.performed += ctx => ToggleCameraLock();
        }

        public void Enable() => controls.Enable();
        public void Disable() => controls.Disable();

        private void ToggleCameraLock() => cameraLocked = !cameraLocked;

        public bool IsCameraLocked() => cameraLocked || (keyboard?.spaceKey?.isPressed ?? false);
    }
}