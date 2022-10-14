using System;
using UnityEngine;

namespace GameResources.Scripts.Input.Base
{
    public interface IInputService
    {
        /// <summary>
        /// Направление движения
        /// </summary>
        public Vector2 MoveDirection { get; }
    
        /// <summary>
        /// Направление взгляда(поворота)
        /// </summary>
        public Vector2 LookDirection { get; }

        /// <summary>
        /// Событие нажатия кнопки способности рывок
        /// </summary>
        public event Action onDashDown;
    }
}