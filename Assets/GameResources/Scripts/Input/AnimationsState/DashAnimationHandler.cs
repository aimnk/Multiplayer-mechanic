using GameResources.Scripts.Base;
using UnityEngine;

namespace GameResources.Scripts.Input.AnimationsState
{
    /// <summary>
    /// Обработчик включения анимации при использование способности рывка
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class DashAnimationHandler : MonoBehaviour
    {
        private const string DASH_KEY = "isDash";

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            Game.InputService.onDashDown += OnDashDown;
        }

        private void OnDestroy() => Game.InputService.onDashDown -= OnDashDown;
        private void OnDashDown() => animator.SetBool(DASH_KEY, true);
    }
}
