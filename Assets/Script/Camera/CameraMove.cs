using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Camera
{
    public class CameraMove
    {
        private readonly float edgeScrollSpeed = 50f;
        private readonly float edgeScrollZonePercent = 0.10f;

        public Vector3 GetScrollOffset(Transform camTransform)
        {
            if (Mouse.current == null) return Vector3.zero;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float xPercent = mousePos.x / screenWidth;
            float yPercent = mousePos.y / screenHeight;

            Vector3 moveDir = Vector3.zero;

            if (xPercent >= 1f - edgeScrollZonePercent) moveDir += ClampFlat(camTransform.right);
            if (xPercent <= edgeScrollZonePercent) moveDir -= ClampFlat(camTransform.right);
            if (yPercent >= 1f - edgeScrollZonePercent) moveDir += ClampFlat(camTransform.forward);
            if (yPercent <= edgeScrollZonePercent) moveDir -= ClampFlat(camTransform.forward);

            return moveDir * edgeScrollSpeed * Time.deltaTime;
        }

        private Vector3 ClampFlat(Vector3 v)
        {
            v.y = 0;
            return v.normalized;
        }
    }
}