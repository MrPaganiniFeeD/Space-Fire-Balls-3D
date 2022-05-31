using Scrpts.CosmicShip;
using Scrpts.CosmicShip.Bullets;
using UnityEngine;

public class ButtonSwitchBulletType : MonoBehaviour
{
    [SerializeField] private BulletType _bulletType;

    private Ship _ship;
    
    public void Construct(Ship ship) => 
        _ship = ship;

    public void OnClick()
    {
        _ship.SwitchBullet(_bulletType);
    }
}
