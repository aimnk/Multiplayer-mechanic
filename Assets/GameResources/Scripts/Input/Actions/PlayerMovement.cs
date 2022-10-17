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
      
      protected override void Awake()
      {
         base.Awake();
         Cursor.visible = false;
      }

      protected override void Action()
      {
         if (PlayerEntity.InputService == null)
         {
            enabled = false;
            return;;
         }
         
         if ((PlayerEntity.InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon))
            return;

         var movementVector = heroCamera.transform.TransformDirection(PlayerEntity.InputService.MoveDirection);
         movementVector.y = 0;
         movementVector.Normalize();
         movementVector += Physics.gravity;
         
         CharacterController.Move(movementVector * (movementSpeed * Time.deltaTime));
      }

      private void Update() => Action();
   }
}
