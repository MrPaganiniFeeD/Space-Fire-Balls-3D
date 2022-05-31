using Scrpts.CosmicShip;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scrpts.Game
{
    public class BootstrapGame : MonoBehaviour
    {
        private const string Ship = "Ship/Ship";
        private const string SceneName = "SampleScene";

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private DamageTrigger _damageTrigger;
        [SerializeField] private ButtonSwitchBulletType _defaultBulletButton;
        [SerializeField] private ButtonSwitchBulletType _rocketBulletButton;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Tower _tower;
        
        private Ship _ship;
        
        public void StartGame()
        {
            Ship ship = SpawnShip();
            InitTower();
            InitUI(ship);
        }

        private void InitTower()
        {
            _tower.SizeUpdate += OnSizeTowerCheck;
        }

        private void OnSizeTowerCheck(int size)
        {
            if(size == 0)
                EndGame();
        }

        private void InitUI(Ship ship)
        {
            _healthView.Construct(ship);
            _damageTrigger.Construct(ship);
            
            _defaultBulletButton.Construct(ship);
            _rocketBulletButton.Construct(ship);
        }

        private Ship SpawnShip()
        {
            GameObject original = Resources.Load<GameObject>(Ship);
            GameObject prefab = Instantiate(original, _spawnPoint.position, Quaternion.identity, null);
            _ship = prefab.GetComponent<Ship>();
            _ship.Death += EndGame;
            return _ship;
        }

        private void EndGame() => 
            SceneManager.LoadScene(SceneName);

        private void OnDestroy()
        {
            _ship.Death -= EndGame;
        }
    }
}