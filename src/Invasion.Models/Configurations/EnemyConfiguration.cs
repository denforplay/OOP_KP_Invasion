namespace Invasion.Models.Configurations;

public class EnemyConfiguration
{
    private int _cost;
    private int _maxHealth;
    private float _speed;

    public int MaxHealth => _maxHealth;
    public float Speed => _speed;
    public int Cost => _cost;
    
    public EnemyConfiguration(int health, float speed, int cost)
    {
        _cost = cost;
        _maxHealth = health;
        _speed = speed;
    }
}