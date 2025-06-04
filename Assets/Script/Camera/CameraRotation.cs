using Script.Game.Player;
using UnityEngine;

namespace Script.Camera
{
    public class CameraRotation
    {
        private readonly Vector3 team1Angles = new Vector3(50f, 35f, 0f);
        private readonly Vector3 team2Angles = new Vector3(50f, 215f, 0f);

        public Vector3 GetRotation()
        {
            int team = LocalPlayer.Instance?.Team ?? 1;
            return team switch
            {
                1 => team1Angles,
                2 => team2Angles,
                _ => team1Angles
            };
        }
    }
}