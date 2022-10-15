using UnityEngine;

namespace GameResources.Scripts.Input.AnimationsState
{
    public class DashState : StateMachineBehaviour
    {
        private const string DASH_KEY = "isDash";
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            animator.SetBool(DASH_KEY, false);
        }
    }
}
