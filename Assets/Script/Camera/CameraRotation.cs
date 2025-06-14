using Script.Game.Player;
using UnityEngine;

namespace Script.Camera
{
    public class CameraRotation
    {
        private readonly Vector3 _team1Angles = new Vector3(50f, 35f, 0f);
        private readonly Vector3 _team2Angles = new Vector3(50f, 215f, 0f);
        private readonly Vector3 _team0Angles = new Vector3(90f, 0f, 0f);

        public Vector3 GetRotation()
        {
            int team = LocalPlayer.Instance.GetControlledEntityComponent().Team ;
            return team switch
            {
                1 => _team1Angles,
                2 => _team2Angles,
                _ => _team0Angles
            };
        }
    }
}