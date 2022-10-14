using GameResources.Scripts.Base;
using GameResources.Scripts.Input.Base;
using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class AbstractAction : MonoBehaviour
    {
        [SerializeField] 
        protected Camera heroCamera;
    
        protected CharacterController CharacterController;
    
        protected IInputService InputService;
    
        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            InputService = Game.InputService;
        }

        protected abstract void Action();
    }
}
