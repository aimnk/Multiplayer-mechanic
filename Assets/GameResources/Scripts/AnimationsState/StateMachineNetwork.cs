using GameResources.Scripts.Networking;
using GameResources.Scripts.Networking.Base;
using UnityEngine;

namespace GameResources.Scripts.AnimationsState
{
    public class StateMachineNetwork : StateMachineBehaviour
    {
        protected PlayerEntity PlayerEntity;

        public new virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerEntity = animator.GetComponentInParent<PlayerEntity>();
        }
    }
}
