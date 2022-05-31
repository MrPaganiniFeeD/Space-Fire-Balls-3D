using Scrpts.CosmicShip;
using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private Ship _ship;

    public void Construct(Ship ship)
    {
        _ship = ship;
        OnChangedHealth(_ship.CurrentHealth);
        _ship.ChangeHealth += OnChangedHealth;
    }

    private void OnChangedHealth(int health) => 
        _text.text = health.ToString();

    private void OnDestroy() =>
        _ship.ChangeHealth -= OnChangedHealth;
}
