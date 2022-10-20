using UnityEngine;
using UnityEngine.InputSystem;

namespace GameResources.Scripts.UI
{
    /// <summary>
    /// Локер курсора
    /// </summary>
    public class CursorLocker : MonoBehaviour
    {
        private PlayerInput playerInput;

        private void Awake() => playerInput = new PlayerInput();

        private void OnEnable()
        {
            playerInput.UI.EscapeButton.performed += OnEscapeButton;
            playerInput.UI.ClickMouse.performed += OnClickMouse;
            playerInput.Enable();
            SetLockCursor(false);
        }

        private void OnDisable()
        {
            playerInput.UI.EscapeButton.performed -= OnEscapeButton;
            playerInput.UI.ClickMouse.performed -= OnClickMouse;
        }

        private void OnClickMouse(InputAction.CallbackContext context) => SetLockCursor(false);

        private void OnEscapeButton(InputAction.CallbackContext context) => SetLockCursor(true);

        private void SetLockCursor(bool state)
        {
            Cursor.visible = state;
            Cursor.lockState =  CursorLockMode.Confined;
        }
    }
}
