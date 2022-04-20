namespace Invasion.Models.Configurations;

/// <summary>
/// Player configuration
/// </summary>
public class PlayerConfiguration
{
    private float _speed;
    private int _maxHealth;

    /// <summary>
    /// Player speed
    /// </summary>
    public float Speed => _speed;

    /// <summary>
    /// Player max health
    /// </summary>
    public int MaxHealth => _maxHealth;


    /// <summary>
    /// Player condifuration constructor
    /// </summary>
    /// <param name="speed">Player speed</param>
    /// <param name="maxHealth">Max health</param>
    public PlayerConfiguration(float speed, int maxHealth)
    {
        _speed = speed;
        _maxHealth = maxHealth;
    }
}