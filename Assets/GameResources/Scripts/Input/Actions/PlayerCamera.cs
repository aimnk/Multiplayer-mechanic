using UnityEngine;

namespace GameResources.Scripts.Input.Actions
{
    /// <summary>
    /// Управление камерой от 3 лица
    /// </summary>
    public class PlayerCamera : AbstractAction
    {
        [SerializeField] 
        private Transform lookAtTransform;
            
        [SerializeField] 
        private float sensitivity = 10f;
        
        private Vector3 rotate;

        private Quaternion angleRotation;

        private Vector3 euler = Vector3.zero;
        
        protected override void Action()
        {
            rotate.x += InputService.LookDirection.x;
            euler.y = CharacterController.transform.rotation.y + rotate.x;
            angleRotation.eulerAngles = euler;
            CharacterController.transform.rotation = angleRotation;
            
            heroCamera.transform.LookAt(lookAtTransform);
            heroCamera.transform.Translate(Vector3.right * (rotate.y * Time.deltaTime * sensitivity));
        }
        
        private void Update() =>  Action();
    }
}
