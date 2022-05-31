using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scrpts.CosmicShip.Bullets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scrpts.CosmicShip
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private List<Transform> _shootPoints;
        [SerializeField] private float _reloadSpeed;
        [SerializeField] private float _recoilDistance;
        [SerializeField] private int _health;

        public Action<int> ChangeHealth;
        public Action Death;

        public int CurrentHealth => _health;


        private BulletType _currentBulletType;
        private bool _isRecharged;
        private BulletFabric _fabric;


        private void Start()
        {
            _fabric = new BulletFabric();
            _isRecharged = true;
            SwitchBullet(BulletType.Default);
        }

        private void Update() => 
            CheckInput();

        public void TakeDamage(int damage)
        {
            if (_health - damage <= 0)
            {
                _health = 0;
                ChangeHealth?.Invoke(_health);
                Death?.Invoke();
                return;
            }
            
            _health -= damage;
            ChangeHealth?.Invoke(_health);
        }

        public void SwitchBullet(BulletType bullet) =>
            _currentBulletType = bullet;

        private void Shoot()
        {
            if (_isRecharged)
            {
                _isRecharged = false;
                IBullet prefabBullet = _fabric.SpawnBullet(_currentBulletType, GetRandomShootPoint());
                
                AnimationRecoil();
                StartCoroutine(AttackCoolDown());
            }
        }

        private void AnimationRecoil()
        {
            transform.DOMoveZ(transform.position.z - _recoilDistance, _reloadSpeed / 2)
                .SetLoops(2, LoopType.Yoyo);
        }

        private Vector3 GetRandomShootPoint()
        {
            int currentPosition = Random.Range(0, _shootPoints.Count);
            return _shootPoints[currentPosition].position;
        }

        private void CheckInput()
        {
            if (Input.GetMouseButton(0)) 
                Shoot();
            
            if (Input.GetMouseButton(1)) 
                SwitchBullet(BulletType.Rocket);
            if (Input.GetKey(KeyCode.Space))
                SwitchBullet(BulletType.Default);
        }

        private IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(_reloadSpeed);
            _isRecharged = true;
        }
    }
}

