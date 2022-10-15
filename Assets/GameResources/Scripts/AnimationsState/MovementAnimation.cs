using GameResources.Scripts.Base;
using UnityEngine;

namespace GameResources.Scripts.Input.AnimationsState
{
    /// <summary>
    /// Состояние анимации - передвижение
    /// </summary>
    public class MovementAnimation : StateMachineBehaviour
    {
        private const string SPEED_KEY = "Speed";

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            animator.SetFloat(SPEED_KEY, Game.InputService.MoveDirection.sqrMagnitude);
        }
    }
}
