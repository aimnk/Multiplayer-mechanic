using System.Collections;
using System.Collections.Generic;
using GameResources.Scripts.Networking.Leaderbord;
using GameResources.Scripts.Networking.View;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Base
{
    /// <summary>
    /// Спавнер игроков
    /// </summary>
    /// TODO: необходимо переписать класс, нарушение SOLID
    public class PlayerSpawner : NetworkBehaviour
    {
        [SerializeField]
        private List<Transform> waypoints;
        
        [SerializeField] 
        private int delaySpawn = 5;

        [SerializeField] 
        private WinPlayerView winPlayerView;
        
        [SerializeField] 
        private DeathPlayerView deathPlayerView;

        [SerializeField] 
        private ViewRestartRound viewRestartRound;

        [SerializeField] 
        private LeaderBoardController leaderBoardController;
        
        [SerializeField]
        private Camera freeCamera;
        
        private RoomNetworkManager networkManager;

        private Coroutine delaySpawnCoroutine;
        
        private void Awake() => networkManager = NetworkManager.singleton as RoomNetworkManager;
        
        /// <summary>
        /// Запустить спавн игроков
        /// </summary>
        public void StartSpawn()
        {
            if (delaySpawnCoroutine != null)
                StopCoroutine(delaySpawnCoroutine);
            
            delaySpawnCoroutine = StartCoroutine(RespawnPlayers());
        }
        
        private IEnumerator RespawnPlayers()
        {
            viewRestartRound.StartCountdown(delaySpawn);
            yield return new WaitForSeconds(delaySpawn);

            foreach (var player in networkManager.Players)
            {
                SpawnPlayer(player);
            }
        }
        
        [ClientRpc]
        private void SpawnPlayer(PlayerEntity player)
        {
            SpawnRandomWaypoint(player);
            SetPropertyPlayer(player);
            freeCamera.gameObject.SetActive(false);
            winPlayerView.ShowWinnerWindow(false);
            deathPlayerView.ShowWindowDeath(false);
            leaderBoardController.ResetStats();
        }

        private void SetPropertyPlayer(PlayerEntity player)
        {
            player.gameObject.SetActive(true);
            player.isImortality = false;
            player.CmdSetHealth(player.DefaultCountHealth);
            player.GetComponentInChildren<ImmortalityView>().SetOutline(false);
        }

        private void SpawnRandomWaypoint(PlayerEntity player) 
            => player.transform.position = waypoints[Random.Range(0, waypoints.Count)].position;
        
    }
}