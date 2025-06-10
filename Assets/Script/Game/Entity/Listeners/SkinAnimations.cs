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

        private void Update()
        {
            if (entityComponent.IsUnityNull()) return;

            string currentSkinAnimation = entityComponent.SkinAnimation;

            if (!string.IsNullOrEmpty(currentSkinAnimation) &&
                currentSkinAnimation != animationName)
            {
                if (animationComponent.IsUnityNull())
                {
                    Debug.LogWarning("[SkinAnimations.cs] Animation component is null, cannot update animation.");
                    return;
                }

                animationName = currentSkinAnimation;
                CrossFadeToAnimation(animationName);
            }
        }

        private void CrossFadeToAnimation(string animName)
        {
            if (HasAnimation(animName))
            {
                animationComponent.CrossFade(animName, 0.2f);
                animationStatus = $"Playing animation: {animName}";
            }
            else
            {
                Debug.LogWarning($"[SkinAnimations.cs] Animation '{animName}' not found in component.");
            }
        }

        private bool HasAnimation(string animName)
        {
            foreach (AnimationState state in animationComponent)
            {
                if (state.name == animName)
                    return true;
            }
            return false;
        }

        public float GetAnimationLength(string animName)
        {
            if (animationComponent.IsUnityNull()) return 0f;

            foreach (AnimationState state in animationComponent)
            {
                if (state.name == animName)
                    return state.length;
            }

            Debug.LogWarning($"Animation '{animName}' not found.");
            return 0f;
        }

        public void UpdateAnimationEntitySkinController(GameObject gameObject_, EntityComponent entityComponent_)
        {
            animationComponent = gameObject_.transform.GetComponentInChildren<Animation>();
            entityComponent = entityComponent_;
            animationName = "";
            animationStatus = "Skin just loaded, waiting for animation to be set.";
        }
    }
}
