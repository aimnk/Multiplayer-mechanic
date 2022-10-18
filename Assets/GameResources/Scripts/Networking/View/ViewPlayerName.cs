using GameResources.Scripts.Networking.Base;
using TMPro;
using UnityEngine;

namespace GameResources.Scripts.Networking.View
{
    /// <summary>
    /// Отображение имени игрока
    /// </summary>
    public class ViewPlayerName : MonoBehaviour
    {
        [SerializeField] 
        private PlayerEntity playerEntity;
    
        [SerializeField] 
        private TMP_Text textName;

        private void Start() => ShowPlayerName();
    
        public void ShowPlayerName() => textName.text = playerEntity.PlayerName;
    }
}
