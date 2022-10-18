using System.Collections;
using System.Collections.Generic;
using GameResources.Scripts.Networking.Base;
using GameResources.Scripts.Networking.View;
using Mirror;
using UnityEngine;

namespace GameResources.Scripts.Networking.Leaderbord
{
   /// <summary>
   /// Контроллер счета игроков
   /// </summary>
   /// TODO: необходимо переписать класс, нарушение SOLID
   public class LeaderBoardController : NetworkBehaviour
   {
      [SerializeField]
      private ViewPlayerStats prefabPlayerStats;

      [SerializeField] 
      private Transform content;

      private RoomNetworkManager networkManager;

      private List<ViewPlayerStats> viewPlayerStatsList = new List<ViewPlayerStats>();
      
      private List<PlayerStatisticData> playerStatDatas = new List<PlayerStatisticData>();
   
      private void Awake() => networkManager = NetworkManager.singleton as RoomNetworkManager;

      private void Start()
      {
         playerStatDatas.Clear();
         StartCoroutine(WaitConnectionPlayers());
      }

      /// <summary>
      /// TODO: необходим коллбек при подключение всех игроков
      /// </summary>
      /// <returns></returns>
      private IEnumerator WaitConnectionPlayers()
      {
         yield return new WaitForSeconds(2);
         
         foreach (var player in networkManager.Players)
         {
            AddPlayerToLeaderBoard(player);
            
            if (player.TryGetComponent(out HitHandler hitHandler))
            {
               hitHandler.onHit += CmdAddHitToStats;
            }
         }
      }
      
      [ServerCallback]
      private void CmdAddHitToStats(PlayerEntity playerEntity) =>  UpdateStatData(playerEntity);

      [ClientRpc]
      private void UpdateStatData(PlayerEntity playerEntity)
      {
         var playerStatData = playerStatDatas.Find(data => data.NamePlayer == playerEntity.PlayerName);

         if (playerStatData != null)
         {
            playerStatData.CountHit++;
         }

         UpdateLeaderboard();
      }

      [ClientRpc]
      private void AddPlayerToLeaderBoard(PlayerEntity playerEntity)
      {
         if (!playerEntity)
            return;
         
         var playerStats = Instantiate(prefabPlayerStats, content);
         viewPlayerStatsList.Add(playerStats);

         var playerStatData = new PlayerStatisticData()
         {
            NamePlayer = playerEntity.PlayerName,
            CountHit = 0
         };
      
         playerStatDatas.Add(playerStatData);
         playerStats.ShowStats(playerStatData);
      }
      
      private void UpdateLeaderboard()
      {
         for (var i = 0; i < viewPlayerStatsList.Count; i++)
         {
            var viewPlayerStats = viewPlayerStatsList[i];

            if (playerStatDatas[i] == null) 
               continue;
         
            viewPlayerStats.ShowStats(playerStatDatas[i]);
         }
      }

      public void ResetStats()
      {
         playerStatDatas.ForEach(data => data.CountHit = 0);
         UpdateLeaderboard();
      }
   }
}
