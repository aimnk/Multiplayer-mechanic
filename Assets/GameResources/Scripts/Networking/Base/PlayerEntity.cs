using System;
using GameResources.Scripts.Input.Base;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Base
{
    /// <summary>
    /// Сущность игрока
    /// </summary>
    public class PlayerEntity : NetworkBehaviour
    {
        /// <summary>
        /// Событие - игрок умер
        /// </summary>
        public event Action<PlayerEntity> onDied = delegate {  };
        
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string PlayerName => playerName;

        /// <summary>
        /// Игрока неуязвим?
        /// </summary>
        public bool IsImortality => isImortality;

        /// <summary>
        /// Стартовое значение здоровья игрока
        /// </summary>
        public int DefaultCountHealth => defaultCountHealth;
        
        public IInputService InputService { get; private set; }
        
        [SyncVar(hook = nameof(SetNamePlayer))]
        private string playerName;

        [SyncVar][SerializeField]
        private int health;

        [SyncVar][SerializeField]
        public bool isImortality;

        [SerializeField]
        private int defaultCountHealth = 3;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            InputService = new SimpleInput();
            CmdSetHealth(defaultCountHealth);
        }

        /// <summary>
        /// Установить режим неуязвимости у игрока
        /// </summary>
        /// <param name="state"></param>
        public void SetImortality(bool state) => isImortality = state;
        
        /// <summary>
        /// Установить здоровье у игрока
        /// </summary>
        /// <param name="count"></param>
        [Command]
        public void CmdSetHealth(int count) => health = count;
        
        /// <summary>
        /// Установить имя игроока
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="namePlayer"></param>
        public void SetNamePlayer(string oldName, string namePlayer) => playerName = namePlayer;
        
        /// <summary>
        /// Отнять здоровьте у игрока
        /// </summary>
        public void DecreaseHealth()
        {
            if (isImortality)
                return;
            
            health--;
            SetImortality(true);
            
            if (health <= 0)
            {
                if (isLocalPlayer)
                    CmdOnDied();
            }
        }

        [Command]
        private void CmdOnDied() => onDied?.Invoke(this);
        
    }
}
