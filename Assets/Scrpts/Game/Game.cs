using UnityEngine;

namespace Scrpts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private BootstrapGame _bootstrapGame;

        private void Awake()
        {
            _bootstrapGame.StartGame();
        }
    }
}