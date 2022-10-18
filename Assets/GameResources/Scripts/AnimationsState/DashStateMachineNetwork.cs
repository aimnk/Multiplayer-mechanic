using UnityEngine;

namespace GameResources.Scripts.AnimationsState
{
    public class DashStateMachineNetwork : StateMachineNetwork
    {
        private const string DASH_KEY = "isDash";
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (!PlayerEntity.hasAuthority)
                return;
            
            animator.SetBool(DASH_KEY, false);
        }
    }
}
