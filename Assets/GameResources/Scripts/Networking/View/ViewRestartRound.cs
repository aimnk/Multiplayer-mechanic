using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;

namespace GameResources.Scripts.Networking.View
{
    /// <summary>
    /// Показать счетчик обратного отсчета до рестарта раунда
    /// </summary>
    public class ViewRestartRound : NetworkBehaviour
    {
        [SerializeField]
        private GameObject viewCountdownWindow;
        
        [SerializeField] 
        private TMP_Text textCountdown;
        
        private Coroutine countdownCoroutine;
        
        /// <summary>
        /// Запустить обратный отсчет
        /// </summary>
        /// <param name="delay"></param>
        [ClientRpc]
        public void StartCountdown(int delay)
        {
            viewCountdownWindow.SetActive(true);
            
            if (countdownCoroutine != null)
                StopCoroutine(countdownCoroutine);

            countdownCoroutine = StartCoroutine(Countdown(delay));
        }

        private IEnumerator Countdown(int delay)
        {
            for (int timeTick = delay; timeTick > 0; timeTick--)
            {
                textCountdown.text = timeTick.ToString();
                yield return new WaitForSeconds(1);
            }
            
            viewCountdownWindow.SetActive(false);
        }
    }
}