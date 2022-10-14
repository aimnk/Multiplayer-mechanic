using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameResources.Scripts.Base
{
    /// <summary>
    /// Entry point game
    /// </summary>
    public class GameBootstrapper : MonoBehaviour
    {
        /// <summary>
        /// Завершение нициализации игры
        /// </summary>
        public event Action onInit = delegate { };
        
        private Game game;

        private void Awake()
        {
            game = new Game();
            onInit.Invoke();
            DontDestroyOnLoad(this);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
