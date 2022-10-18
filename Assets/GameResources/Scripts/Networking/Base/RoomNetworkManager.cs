using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Base
{
    public class RoomNetworkManager : NetworkRoomManager
    {
        /// <summary>
        /// Список игроков
        /// </summary>
        public IReadOnlyList<PlayerEntity> Players => players;
        
        [SerializeField] 
        private List<PlayerEntity> players = new List<PlayerEntity>();
        
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            var player = gamePlayer.GetComponent<PlayerEntity>();
            var networkRoomPlayer = roomPlayer.GetComponent<Mirror.NetworkRoomPlayer>();
            int numberPlayer = networkRoomPlayer.index + 1;
            player.SetNamePlayer(String.Empty, $"Player {numberPlayer}");
            players.Add(player);
            return true;
        }

        public override void OnRoomServerSceneChanged(string sceneName)
        {
            base.OnRoomServerSceneChanged(sceneName);
            players.Clear();
        }
    }
}
