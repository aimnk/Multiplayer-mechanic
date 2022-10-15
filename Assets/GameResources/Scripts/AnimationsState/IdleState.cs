using GameResources.Scripts.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameResources.Scripts.Input.AnimationsState
{
    /// <summary>
    /// Состояние анимации idle
    /// </summary>
    public class IdleState : StateMachineBehaviour
    {
        private const string SPEED_KEY = "Speed";
        private const string IDLE_INDEX_KEY = "IdleIndex";

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetInteger(IDLE_INDEX_KEY, Random.Range(1, 4));
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            animator.SetFloat(SPEED_KEY, Game.InputService.MoveDirection.sqrMagnitude);
        }
    }
}
