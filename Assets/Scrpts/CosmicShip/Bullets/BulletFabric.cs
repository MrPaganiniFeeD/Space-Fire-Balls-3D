using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scrpts.CosmicShip.Bullets
{
    public class BulletFabric
    {
        private const string RocketBulletPath = "Ship/Bullet/Rocket";
        private const string DefaultBulletPath = "Ship/Bullet/Default";

        public IBullet SpawnBullet(BulletType bulletType ,Vector3 spawnPoint)
        {
            return bulletType switch
            {
                BulletType.Default => GetBullet(DefaultBulletPath, spawnPoint),
                BulletType.Rocket => GetBullet(RocketBulletPath, spawnPoint),
                _ => throw new ArgumentOutOfRangeException(nameof(bulletType), bulletType, null)
            };
        }

        private static IBullet GetBullet(string prefabPath ,Vector3 spawnPoint, Quaternion quaternion = new Quaternion())
        {
            GameObject gameObject = Resources.Load<GameObject>(prefabPath);
            GameObject prefab = Object.Instantiate(gameObject, spawnPoint, gameObject.transform.rotation);
            IBullet bullet = prefab.GetComponent<IBullet>();
            return bullet;
        }
    }

    public enum BulletType
    {
        Default,
        Rocket
    }
}
