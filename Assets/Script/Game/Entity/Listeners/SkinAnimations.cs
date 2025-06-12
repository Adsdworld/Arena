using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Game.Entity.Listeners
{
    public class SkinAnimations : MonoBehaviour
    {
        [SerializeField] private EntityComponent entityComponent;
        [SerializeField] private string animationName;
        [SerializeField] private string animationStatus;
        [SerializeField] private Animation animationComponent;
        [SerializeField] private float animationSpeed;

        private void Update()
        {
            if (entityComponent.IsUnityNull()) return;

            string currentSkinAnimation = entityComponent.SkinAnimation;

            if (!string.IsNullOrEmpty(currentSkinAnimation) &&
                currentSkinAnimation != animationName)
            {
                if (animationComponent.IsUnityNull())
                {
                    Debug.LogWarning("Animation component is null, cannot update animation.");
                    return;
                }

                animationName = currentSkinAnimation;
                animationSpeed = entityComponent.SkinAnimationSpeed;
                CrossFadeToAnimation(animationName);
            }
        }

        private void CrossFadeToAnimation(string animName)
        {
            if (animName == "None") return;
            if (HasAnimation(animName))
            {
                animationComponent[animName].speed = animationSpeed;
                animationComponent.CrossFade(animName, 0.2f);
                animationStatus = $"Playing animation: {animName}";
            }
            else
            {
                Debug.LogWarning($"Animation '{animName}' not found in component.");
            }
        }

        private bool HasAnimation(string animName)
        {
            if (animName == "None") return false;
            foreach (AnimationState state in animationComponent)
            {
                if (state.name == animName)
                    return true;
            }
            return false;
        }

        public void UpdateAnimationEntitySkinController(GameObject gameObject_, EntityComponent entityComponent_)
        {
            Log.Info($"Updating animation entity skin controller for {gameObject_.name} with entity component {entityComponent_.Name}.");
            animationComponent = gameObject_.transform.GetComponentInChildren<Animation>();
            entityComponent = entityComponent_;
            animationName = "";
            animationStatus = "Skin just loaded, waiting for animation to be set.";
        }
    }
}
