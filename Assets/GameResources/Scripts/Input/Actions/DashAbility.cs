using System.Collections;
using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    /// <summary>
    /// Способность - рывок
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class DashAbility : AbstractAction
    {
        private const float DASH_TIME = 0.1f;

        private const  float TIME_DELAY = 0.01f;
        
        [SerializeField]
        private int distanceDash = 10;
        
        private Coroutine dashCoroutine;
        
        protected override void Awake()
        {
            base.Awake();
            InputService.onDashDown += Action;
        }

        private void OnDestroy() => InputService.onDashDown -= Action;

        protected override void Action()
        {
            if (dashCoroutine != null)
                StopCoroutine(dashCoroutine);
            
            dashCoroutine = StartCoroutine(DashMove());
        }

        private IEnumerator DashMove()
        {
            if (InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon)
                yield return null;
            
            var movementVector = heroCamera.transform.TransformDirection(InputService.MoveDirection);
            movementVector.y = 0;
            movementVector.Normalize();
            
            for (float time = 0; time < DASH_TIME; time += TIME_DELAY)
            {
                CharacterController.Move(movementVector * (distanceDash * time));
                yield return new WaitForSeconds(TIME_DELAY);
            }
            
            yield return null;
        }
    }
}
