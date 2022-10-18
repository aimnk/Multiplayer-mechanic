using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.View
{
    /// <summary>
    /// Визаулизация для игрока при наступлении смерти
    /// </summary>
    public class DeathPlayerView : NetworkBehaviour
    {
        [SerializeField] 
        private GameObject deathWindow;
        
        [SerializeField]
        private Camera Camera;

        
        /// <summary>
        /// Отправить клиенту визуализацию смерти
        /// </summary>
        /// <param name="conn"></param>
        [TargetRpc]
        public void SendClientOnDied(NetworkConnection conn) => ShowWindowDeath(true);

        
        /// <summary>
        /// Показать окно смерти
        /// </summary>
        public void ShowWindowDeath(bool state)
        {
            deathWindow.SetActive(state);
            Camera.gameObject.SetActive(state);
        }
    }
}