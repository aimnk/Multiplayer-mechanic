using UnityEngine;
using Random = UnityEngine.Random;

namespace GameResources.Scripts.AnimationsState
{
    /// <summary>
    /// Состояние анимации idle
    /// </summary>
    public class IdleState : StateMachineNetwork
    {
        private const string SPEED_KEY = "Speed";
        private const string IDLE_INDEX_KEY = "IdleIndex";
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            if (!PlayerEntity.hasAuthority)
                return;
            
            animator.SetInteger(IDLE_INDEX_KEY, Random.Range(1, 4));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            if (PlayerEntity.InputService == null)
                return;
            
            animator.SetFloat(SPEED_KEY, PlayerEntity.InputService.MoveDirection.sqrMagnitude);
        }
    }
}
