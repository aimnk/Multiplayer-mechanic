using GameResources.Scripts.Networking;
using GameResources.Scripts.Networking.Base;
using UnityEngine;

namespace GameResources.Scripts.AnimationsState
{
    /// <summary>
    /// Обработчик включения анимации при использование способности рывка
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class DashAnimationHandler : MonoBehaviour
    {
        private const string DASH_KEY = "isDash";

        private Animator animator;

        private PlayerEntity playerEntity;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            playerEntity = GetComponentInParent<PlayerEntity>();
            
            if (playerEntity.InputService == null)
                return;
            
            playerEntity.InputService.onDashDown += OnDashDown;
        }

        private void OnDestroy()
        {
            if (playerEntity.InputService != null)
                playerEntity.InputService.onDashDown -= OnDashDown;
        }

        private void OnDashDown()
        {
            if (playerEntity.InputService.MoveDirection.sqrMagnitude > Mathf.Epsilon)
                animator.SetBool(DASH_KEY, true);
        }
    }
}
