using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameResources.Scripts.Networking.View;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Base
{
    /// <summary>
    /// Обработчик смерти игроков
    /// </summary>
    public class DeathHandler : NetworkBehaviour
    {
        [SerializeField] 
        private PlayerSpawner playerSpawner;

        [SerializeField] 
        private WinPlayerView winPlayerView;

        [SerializeField]
        private DeathPlayerView deathPlayerView;
    
        private RoomNetworkManager networkManager;
    
        private List<PlayerEntity> playerEntitiesAlive = new List<PlayerEntity>();

        private void Awake() => networkManager = NetworkManager.singleton as RoomNetworkManager;

        public override void OnStopClient()
        {
            base.OnStopClient();
            UnSubscribe();
        }
        private void Start()
        {
            StartCoroutine(WaitConnectionPlayers());
        }
        
        /// <summary>
        /// TODO: необходим коллбек при подключение всех игроков
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitConnectionPlayers()
        {
            yield return new WaitForSeconds(2);

            playerEntitiesAlive = new List<PlayerEntity>(networkManager.Players);
            Subscribe();
        }

        private void Subscribe()
        {
            foreach (var player in networkManager.Players)
            {
                player.onDied += OnDied;
            }
        }
    
        private void UnSubscribe()
        {
            foreach (var player in networkManager.Players)
            {
                player.onDied -= OnDied;
            }
        }
    
        [ServerCallback]
        private void OnDied(PlayerEntity playerEntity)
        {
            if (!playerEntity.TryGetComponent(out NetworkIdentity networkIdentity))
                return;
            
            deathPlayerView.SendClientOnDied(networkIdentity.connectionToClient);
            SetPlayerOnDied(playerEntity, false);
            playerEntitiesAlive.Remove(playerEntity);
            CheckAlive();
        }
    
        private void CheckAlive()
        {
            if (playerEntitiesAlive.Count != 1) 
                return;
        
            var winnerPlayer = playerEntitiesAlive.FirstOrDefault()?.PlayerName;
            winPlayerView.ShowWinner(winnerPlayer);
            playerSpawner.StartSpawn();
            playerEntitiesAlive = new List<PlayerEntity>(networkManager.Players);
        }
    
        [ClientRpc]
        private void SetPlayerOnDied(PlayerEntity playerEntity, bool state) => playerEntity.gameObject.SetActive(state);
    }
}
