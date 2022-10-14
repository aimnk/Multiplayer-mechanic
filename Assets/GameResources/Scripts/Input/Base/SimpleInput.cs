using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameResources.Scripts.Input.Base
{
    /// <summary>
    /// Получение ввода от New Input System
    /// </summary>
    public class SimpleInput : IInputService
    {
        public Vector2 MoveDirection { get; private set; }
    
        public Vector2 LookDirection { get; private set; }
        
        public event Action onDashDown = delegate {  };

        private readonly PlayerInput input;

        public SimpleInput()
        {
            input = new PlayerInput();
            input.Enable();
            Subscribes();
        }
        
        private void Subscribes()
        {
            input.Player.Dash.performed +=  OnDashDown;
            input.Player.Move.performed += OnMove;
            input.Player.Look.performed += OnLock;
            input.Player.Move.canceled += OnMoveCancel;
            input.Player.Look.canceled += OnLookCancel;
        }
    
        private void Unsubscribes()
        {
            input.Player.Dash.performed -=  OnDashDown;
            input.Player.Move.performed -= OnMove;
            input.Player.Look.performed -= OnLock;
            input.Player.Move.canceled -= OnMoveCancel;
            input.Player.Look.canceled -= OnLookCancel;
        }

        private void OnMoveCancel(InputAction.CallbackContext context) => MoveDirection = Vector2.zero;
    
        private void OnLookCancel(InputAction.CallbackContext context) => LookDirection = Vector2.zero;
    
        private void OnLock(InputAction.CallbackContext context) => LookDirection = context.ReadValue<Vector2>();
    
        private void OnMove(InputAction.CallbackContext context) =>  MoveDirection = context.ReadValue<Vector2>();

        private void OnDashDown(InputAction.CallbackContext context) => onDashDown.Invoke();


        ~SimpleInput () => Unsubscribes();
    }
}
