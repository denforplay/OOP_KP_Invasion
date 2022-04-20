namespace Invasion.Models.Configurations;

/// <summary>
/// Enemy configuration
/// </summary>
public class EnemyConfiguration
{
    private int _cost;
    private int _maxHealth;
    private float _speed;
    /// <summary>
    /// Enemy max health
    /// </summary>
    public int MaxHealth => _maxHealth;

    /// <summary>
    /// Enemy speed
    /// </summary>
    public float Speed => _speed;

    /// <summary>
    /// Enemy cost
    /// </summary>
    public int Cost => _cost;
    
    /// <summary>
    /// Enemy configuration constructor
    /// </summary>
    /// <param name="health">Max health</param>
    /// <param name="speed">Speed</param>
    /// <param name="cost">Cost</param>
    public EnemyConfiguration(int health, float speed, int cost)
    {
        _cost = cost;
        _maxHealth = health;
        _speed = speed;
    }
}