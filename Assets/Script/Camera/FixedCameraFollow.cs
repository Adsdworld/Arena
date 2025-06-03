using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Camera
{
    public class FixedCameraFollow : MonoBehaviour
    {
        public Transform target;
        public float followSpeed = 10f;

        private bool LockCamera = true;

        public Vector3 fixedEulerAngles = new Vector3(50f, 35f, 0f);
        public Vector3 offset = new Vector3(0f, 0f, -30f);

        public float edgeScrollSpeed = 10f;
        public float edgeScrollZonePercent = 0.10f; // ✅ 10%

        private Vector3 edgeScrollOffset = Vector3.zero;

        private Keyboard keyboard;
        
        
        private InputSystem_Actions controls;


        void Awake()
        {
            keyboard = Keyboard.current;
            
            controls = new InputSystem_Actions();
            controls.Player.LockCamera.performed += ctx => ToggleCameraLock();
        }
        
        private void ToggleCameraLock()
        {
            LockCamera = !LockCamera;
        }
        
        void OnEnable()
        {
            controls.Enable();
        }
        
        void OnDisable()
        {
            controls.Disable();
        }

        void LateUpdate()
        {
            if (target == null) return;

            HandleEdgeScrolling();

            // Appliquer offset selon la rotation fixe + offset edge
            Vector3 rotatedOffset = Quaternion.Euler(fixedEulerAngles) * offset;
            Vector3 desiredPosition = target.position + rotatedOffset + edgeScrollOffset;

            // Suivi fluide
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Orientation fixe
            transform.rotation = Quaternion.Euler(fixedEulerAngles);
        }

        void HandleEdgeScrolling()
        {
            if (Mouse.current == null) return;
            
            if (keyboard.spaceKey.isPressed || LockCamera)
            {
                edgeScrollOffset = Vector3.zero;
                return;
            }

            Vector2 mousePos = Mouse.current.position.ReadValue();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float xPercent = mousePos.x / screenWidth;
            float yPercent = mousePos.y / screenHeight;

            Vector3 moveDir = Vector3.zero;

            if (xPercent >= 1f - edgeScrollZonePercent)
                moveDir += ClampFlat(transform.right);

            if (xPercent <= edgeScrollZonePercent)
                moveDir -= ClampFlat(transform.right);

            if (yPercent >= 1f - edgeScrollZonePercent)
                moveDir += ClampFlat(transform.forward);

            if (yPercent <= edgeScrollZonePercent)
                moveDir -= ClampFlat(transform.forward);

            edgeScrollOffset += moveDir * edgeScrollSpeed * Time.deltaTime;
        }

        Vector3 ClampFlat(Vector3 v)
        {
            v.y = 0;
            return v.normalized;
        }
    }
}