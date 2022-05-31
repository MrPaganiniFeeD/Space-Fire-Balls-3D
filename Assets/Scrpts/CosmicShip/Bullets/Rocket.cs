using UnityEngine;

namespace Scrpts.CosmicShip.Bullets
{
    public class Rocket : MonoBehaviour, IBullet
    {
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _boomEffect;
        [SerializeField] private int _damage;
        public int Damage => _damage;
        
        private Vector3 _direction;
        private void Start() => 
            _direction = Vector3.up;

        private void Update() => 
            Move();

        private void CollisionHandling()
        {
            ParticleSystemRenderer renderer = Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation)
                .GetComponent<ParticleSystemRenderer>();
        }

        public void InstantiateBullet(Vector3 spawnPoint) => 
            Instantiate(gameObject, spawnPoint, transform.rotation);

        private void Move() => 
            transform.Translate(_direction * _speed * Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Block block))
            {
                block.Break();
                Destroy(gameObject);
            }
            if (other.TryGetComponent(out Obstacle obstacle))
            {
                CollisionHandling();
                obstacle.Destroy();
            }
        }
    }
}
