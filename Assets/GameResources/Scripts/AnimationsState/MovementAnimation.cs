using UnityEngine;

namespace GameResources.Scripts.AnimationsState
{
    /// <summary>
    /// Состояние анимации - передвижение
    /// </summary>
    public class MovementAnimation : StateMachineNetwork
    {
        private const string SPEED_KEY = "Speed";
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            if (PlayerEntity.InputService == null)
                return;
            
            animator.SetFloat(SPEED_KEY, PlayerEntity.InputService.MoveDirection.sqrMagnitude);
        }
    }
}
