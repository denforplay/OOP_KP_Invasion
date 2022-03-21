namespace Invasion.Models.Configurations;

public class PlayerConfiguration
{
    private float _speed;
    private int _maxHealth;

    public float Speed => _speed;
    public int MaxHealth => _maxHealth;

    public PlayerConfiguration(float speed, int maxHealth)
    {
        _speed = speed;
        _maxHealth = maxHealth;
    }
}