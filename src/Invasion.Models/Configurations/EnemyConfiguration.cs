namespace Invasion.Models.Configurations;

public class EnemyConfiguration
{
    private int _maxHealth;
    private float _speed;

    public int MaxHealth => _maxHealth;
    public float Speed => _speed;
    
    public EnemyConfiguration(int health, float speed)
    {
        _maxHealth = health;
        _speed = speed;
    }
}