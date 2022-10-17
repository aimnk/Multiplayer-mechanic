using GameResources.Scripts.Networking;
using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    /// <summary>
    /// Абстракция действий игрока
    /// </summary>
    [RequireComponent(typeof(CharacterController), typeof(PlayerEntity))]
    public abstract class AbstractAction : MonoBehaviour
    {
        [SerializeField] 
        protected Camera heroCamera;

        protected PlayerEntity PlayerEntity;

        protected CharacterController CharacterController;

        protected virtual void Awake()
        {
            PlayerEntity = GetComponent<PlayerEntity>();
            CharacterController = GetComponent<CharacterController>();

            if (!PlayerEntity.isLocalPlayer)
                heroCamera.enabled = false;
        }

        private void Start()
        {
            if (PlayerEntity.hasAuthority)
            {
                heroCamera.enabled = true;
            }
        }

        protected abstract void Action();
    }
}
