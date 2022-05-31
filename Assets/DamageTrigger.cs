using System;
using Scrpts;
using Scrpts.CosmicShip;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;

    private Ship _ship;

    public void Construct(Ship ship)
    {
        _ship = ship;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IBullet bullet))
        {
            _ship.TakeDamage(bullet.Damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (_collider == false)
            return;

        Gizmos.color = new Color32(200, 30, 20, 130);
        Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
    }
}