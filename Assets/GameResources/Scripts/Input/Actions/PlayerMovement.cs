using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
   /// <summary>
   /// Передвижение игрока
   /// </summary>
   public class PlayerMovement : AbstractAction
   {
      [SerializeField]
      private float movementSpeed = 10f;
      
      private Vector3 moveDirection;
      
      protected override void Action()
      {
         if (PlayerEntity.InputService == null)
         {
            enabled = false;
            return;;
         }
         
         if ((PlayerEntity.InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon))
            return;
         
         moveDirection = heroCamera.transform.forward * PlayerEntity.InputService.MoveDirection.y +
                         heroCamera.transform.right * PlayerEntity.InputService.MoveDirection.x;
         
         moveDirection.y = 0;
         
         moveDirection.Normalize();
         moveDirection += Physics.gravity;
         CharacterController.Move(moveDirection * (movementSpeed * Time.deltaTime));
      }

      private void Update() => Action();
   }
}
