using Script.Game.Player;
using UnityEngine;
using WebSocketSharp;

namespace Script.Camera
{
    public class Camera : MonoBehaviour
    {
        //public Transform target;
        public float followSpeed = 10f;
        public Vector3 offset = new Vector3(0f, 0f, -30f);
        
        public float zoomSpeed = 10f;
        public float minZoom = -50f;
        public float maxZoom = -10f;


        private Vector3 edgeScrollOffset = Vector3.zero;

        private CameraMove _cameraMove;
        private CameraRotation _rotationHandler;
        private CameraInput _inputHandler;

        private void Awake()
        {
            _cameraMove = new CameraMove();
            _rotationHandler = new CameraRotation();
            _inputHandler = new CameraInput();
        }

        private void OnEnable() => _inputHandler.Enable();
        private void OnDisable() => _inputHandler.Disable();

        private void LateUpdate()
        {
            GameObject target = LocalPlayer.Instance?.GetControlledEntity();
            
            if (target == null) return;
            
            float scroll = UnityEngine.InputSystem.Mouse.current.scroll.ReadValue().y;
            offset.z += scroll * zoomSpeed * Time.deltaTime;
            offset.z = Mathf.Clamp(offset.z, minZoom, maxZoom);

            if (!_inputHandler.IsCameraLocked())
            {
                edgeScrollOffset += _cameraMove.GetScrollOffset(transform);
            }
            else
            {
                edgeScrollOffset = Vector3.zero;
            }

            Vector3 rotatedOffset = Quaternion.Euler(_rotationHandler.GetRotation()) * offset;
            Vector3 desiredPosition = target.transform.position + rotatedOffset + edgeScrollOffset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(_rotationHandler.GetRotation());
        }
    }
}