using GameResources.Scripts.Input.Base;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking
{
    /// <summary>
    /// Сущность игрока
    /// </summary>
    public class PlayerEntity : NetworkBehaviour
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string PlayerName => playerName;

        /// <summary>
        /// Игрока неуязвим?
        /// </summary>
        public bool IsImortality => isImortality;
        
        [SyncVar(hook = nameof(SetNamePlayer))]
        private string playerName;

        [SyncVar][SerializeField]
        private int health;

        [SyncVar][SerializeField]
        public bool isImortality;

        [SerializeField]
        private int defaultCountHealth = 3;
        
        public IInputService InputService { get; private set; }
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            InputService = new SimpleInput();
            health = defaultCountHealth;
        }

        /// <summary>
        /// Установить режим неуязвимости у игрока
        /// </summary>
        /// <param name="state"></param>
        public void SetImortality(bool state) => isImortality = state;
        
        /// <summary>
        /// Отнять здоровьте у игрока
        /// </summary>
        public void DecreaseHealth()
        {
            if (health <= 0)
            {
              
                Debug.Log("Смэрть");
                return;
            }
            health--;
            SetImortality(true);
        }
        
        /// <summary>
        /// Установить имя игроока
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="namePlayer"></param>
        public void SetNamePlayer(string oldName, string namePlayer) => playerName = namePlayer;
    }
}
