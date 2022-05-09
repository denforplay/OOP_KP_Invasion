using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics.GraphicTargets;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using SharpDX;
using Transform = Invasion.Engine.Components.Transform;

namespace Invasion.View;

/// <summary>
/// Represents view of healthable object
/// </summary>
public class HealthView : IView
{
    private Transform _parent;
    private GameObject _gameObject;
    private IHealthable _healthable;
    private TextRenderer _healthText;

    /// <summary>
    /// Health view constructor
    /// </summary>
    /// <param name="healthableObject">Healthable object</param>
    /// <param name="renderTarget">Render target</param>
    /// <exception cref="Exception"></exception>
    public HealthView(GameObject healthableObject, IGraphicTarget renderTarget)
    {
        _healthable = healthableObject as IHealthable;
        if (_healthable is null)
        {
            throw new Exception();
        }

        healthableObject.TryTakeComponent(out _parent);
        _healthText = new TextRenderer(renderTarget.Target, new RectangleF());
        _healthText.SetText($"{_healthable.CurrentHealthPoints}/{_healthable.MaxHealthPoint} HP");
        _healthable.OnHealthChanged += ChangeText;
    }

    public void Dispose()
    {
        _healthText.Dispose();
    }

    public void Update()
    {
        _healthText.SetPosition(new RectangleF(_parent.Position.X / 45f * Screen.Width - 50, Screen.Height - _parent.Position.Y / 25f * Screen.Height - 30, 100, 100));
        _healthText.Update();
    }

    private void ChangeText(int currentHealth)
    {
        _healthText.SetText($"{currentHealth}/{_healthable.MaxHealthPoint} HP");
    }
    
}