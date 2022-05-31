using System.Collections;
using UnityEngine;

namespace Scrpts
{
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bounceForce;
        [SerializeField] private float _bounceRadius;
        [SerializeField] private int _damage;
        public int Damage => _damage;
        
        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;
        private Transform _transform;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        private void Start() => 
            _moveDirection = Vector3.forward;

        private void Update() => 
            Move();

        private void Move() => 
            transform.Translate(_moveDirection * _speed * Time.deltaTime);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Block block))
            {
                block.Break();
                DestroyBullet();
            }

            if (other.TryGetComponent(out Obstacle obstacle))
            {
                CollisionHandling();
                StartCoroutine(BulletDestroy());
            }
        }

        private void CollisionHandling()
        {
            _moveDirection = Vector3.up + Vector3.back; 
        
            _rigidbody.isKinematic = false;
            _rigidbody.AddExplosionForce(_bounceForce, _transform.position + new Vector3(0, -1, 1), _bounceRadius);
            
        }

        private void DestroyBullet() => 
            Destroy(gameObject);

        private IEnumerator BulletDestroy()
        {
            yield return new WaitForSeconds(0.9f);
            Destroy(gameObject);
        }
    }
}