using System;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.View
{
    public class RoomNetworkManager : NetworkRoomManager
    {
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            var player = gamePlayer.GetComponent<PlayerEntity>();
            var networkRoomPlayer = roomPlayer.GetComponent<Mirror.NetworkRoomPlayer>();
            int numberPlayer = networkRoomPlayer.index + 1;
            player.SetNamePlayer(String.Empty, $"Player {numberPlayer}");
            return true;
        }
    }
}
