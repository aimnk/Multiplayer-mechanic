using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    /// <summary>
    /// Управление камерой от 3 лица
    /// </summary>
    public class PlayerCamera : AbstractAction
    {
        private Vector3 moveDirection;

        private Quaternion angleRotation;
        
        private float angle;

        protected override void Action()
        {
            if (PlayerEntity.InputService == null)
            {
                enabled = false;
                return;;
            }
            
            moveDirection = PlayerEntity.InputService.MoveDirection.normalized;
            angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + heroCamera.transform.eulerAngles.y;
            CharacterController.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        
        private void Update() =>  Action();
    }
}
