using GameResources.Scripts.Networking.Leaderbord;
using GameResources.Scripts.Networking.View;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Base
{
    /// <summary>
    /// Обработчик победителей
    /// </summary>
    public class WinHandler : NetworkBehaviour
    {
        [SerializeField]
        private int countHitToWin = 3;

        [SerializeField] 
        private WinPlayerView winPlayerView;
        
        [SerializeField] 
        private PlayerSpawner playerSpawner;

        [SerializeField]
        private LeaderBoardController leaderBoardController;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            leaderBoardController.onChangeScore += CheckStats;
            playerSpawner.onStartSpawn += DisableWinWindow;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            leaderBoardController.onChangeScore += CheckStats;
            playerSpawner.onStartSpawn -= DisableWinWindow;
        }

        [ServerCallback]
        private void DisableWinWindow() => DisableWindow();
        
        [ClientRpc]
        private void DisableWindow() => winPlayerView.ShowWinnerWindow(false);
        
        [ServerCallback]
        private void CheckStats(int currentScore, PlayerEntity player)
        {
            if (currentScore < countHitToWin)
                return;
            
            SetWinner(player);
        }

        [ClientRpc]
        private void SetWinner(PlayerEntity player)
        {
            var winnerPlayer = player.PlayerName;
            winPlayerView.ShowWinner(winnerPlayer);
            playerSpawner.StartSpawn();
        }
    }
}