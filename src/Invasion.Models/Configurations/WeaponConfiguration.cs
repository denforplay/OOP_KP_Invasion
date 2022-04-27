namespace Invasion.Models.Configurations;

public class WeaponConfiguration
{
    private float _speed;
    private float _reloadTime;
    private int _damage;

    public WeaponConfiguration(float speed, float reloadTime, int damage)
    {
        _speed = speed;
        _reloadTime = reloadTime;
        _damage = damage;
    }

    public int Damage => _damage;
    public float Speed => _speed;
    public float ReloadTime => _reloadTime;
}