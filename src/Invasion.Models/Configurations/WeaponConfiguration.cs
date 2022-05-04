namespace Invasion.Models.Configurations;

/// <summary>
/// Represents weapon configuration
/// </summary>
public class WeaponConfiguration
{
    private float _speed;
    private float _reloadTime;
    private int _damage;

    /// <summary>
    /// Weapon configuration constructor
    /// </summary>
    /// <param name="speed">Weapon speed</param>
    /// <param name="reloadTime">Weapon reload time</param>
    /// <param name="damage">Weapon damage</param>
    public WeaponConfiguration(float speed, float reloadTime, int damage)
    {
        _speed = speed;
        _reloadTime = reloadTime;
        _damage = damage;
    }

    /// <summary>
    /// Weapon damage
    /// </summary>
    public int Damage => _damage;

    /// <summary>
    /// Weapon speed
    /// </summary>
    public float Speed => _speed;

    /// <summary>
    /// Weapon reload time
    /// </summary>
    public float ReloadTime => _reloadTime;
}