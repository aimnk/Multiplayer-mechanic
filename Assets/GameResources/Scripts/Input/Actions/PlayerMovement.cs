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
         if ((InputService.MoveDirection.sqrMagnitude < Mathf.Epsilon))
            return;

         var movementVector = heroCamera.transform.TransformDirection(InputService.MoveDirection);
         movementVector.y = 0;
         movementVector.Normalize();

         CharacterController.Move(movementVector * (movementSpeed * Time.deltaTime));
      }

      private void Update() => Action();
   }
}
