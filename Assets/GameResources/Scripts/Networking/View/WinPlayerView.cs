using Mirror;
using TMPro;
using UnityEngine;

namespace GameResources.Scripts.Networking.View
{
    public class WinPlayerView : NetworkBehaviour
    {
        [SerializeField] 
        private TMP_Text WinnerName;

        private const string TEXT_FORMAT = "{0} WON!";
            
        /// <summary>
        /// Назначить игрока победителем
        /// </summary>
        /// <param name="playerName"></param>
 
        [ClientRpc]
        public void ShowWinner(string playerName)
        {
            ShowWinnerWindow(true);
            WinnerName.text = string.Format(TEXT_FORMAT, playerName);
        }

        /// <summary>
        /// Показать окно победителя
        /// </summary>
        /// <param name="state"></param>
        public void ShowWinnerWindow(bool state) =>   WinnerName.gameObject.SetActive(state);

    }
}