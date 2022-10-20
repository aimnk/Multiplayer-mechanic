using Cinemachine;
using GameResources.Scripts.Networking;
using GameResources.Scripts.Networking.Base;
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

        [SerializeField] 
        protected GameObject cinemachineCamera;
        
        protected PlayerEntity PlayerEntity;

        protected CharacterController CharacterController;

        protected virtual void Awake()
        {
            PlayerEntity = GetComponent<PlayerEntity>();
            CharacterController = GetComponent<CharacterController>();

            if (!PlayerEntity.isLocalPlayer)
            {
                heroCamera.enabled = false;
                cinemachineCamera.SetActive(false);
            }
        }

        private void Start()
        {
            if (PlayerEntity.hasAuthority)
            {
                heroCamera.enabled = true;
                cinemachineCamera.SetActive(true);
            }
        }

        protected abstract void Action();
    }
}
