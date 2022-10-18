using TMPro;
using UnityEngine;

namespace GameResources.Scripts.Networking.Leaderbord
{
    /// <summary>
    /// Отображение статистики игрока
    /// </summary>
    public class ViewPlayerStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text playerName;
    
        [SerializeField]
        private TMP_Text countHit;
    
        /// <summary>
        /// Отобразить статистику игрока
        /// </summary>
        /// <param name="playerStatData"></param>
        public void ShowStats(PlayerStatisticData playerStatData)
        {
            playerName.text = playerStatData.NamePlayer;
            countHit.text = playerStatData.CountHit.ToString();
        }
    }
}
