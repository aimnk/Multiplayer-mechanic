using System;
using System.Collections;
using GameResources.Scripts.Networking;
using Mirror.Examples.Chat;
using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    /// <summary>
    /// Способность - рывок
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class DashAbility : AbstractAction
    {
        public event Action<PlayerEntity> onHitOtherPlayer = delegate {  };
        
        private const float DASH_TIME = 0.1f;

        private const  float TIME_DELAY = 0.01f;
        
        [SerializeField]
        private int distanceDash = 10;
        
        private Coroutine dashCoroutine;

        private bool isDash = false;

        private bool alreadyHit = false;
        
        private void Start()
        {
            if (PlayerEntity.InputService == null)
                return;
            ;
            PlayerEntity.InputService.onDashDown += Action;
        }

        private void OnDestroy()
        {
            if (PlayerEntity.InputService != null)
                PlayerEntity.InputService.onDashDown -= Action;
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!isDash || alreadyHit)
                return;

            if (hit.collider.TryGetComponent(out PlayerEntity otherPlayer))
            {
                alreadyHit = true;
                onHitOtherPlayer.Invoke(otherPlayer);
            }
        }

        protected override void Action()
        {
            if (PlayerEntity.InputService == null)
            {
                enabled = false;
                return;;
            }
            
            if (dashCoroutine != null)
                StopCoroutine(dashCoroutine);

            dashCoroutine = StartCoroutine(DashMove());
        }

        private IEnumerator DashMove()
        {
            alreadyHit = false;
            
            if (PlayerEntity.InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon)
                yield return null;

            isDash = true;
            var movementVector = heroCamera.transform.TransformDirection(PlayerEntity.InputService.MoveDirection);
            movementVector.y = 0;
            movementVector.Normalize();
            
            for (float time = 0; time < DASH_TIME; time += TIME_DELAY)
            {
                CharacterController.Move(movementVector * (distanceDash * time));
                yield return new WaitForSeconds(TIME_DELAY);
            }

            isDash = false;
            yield return null;
        }
    }
}
