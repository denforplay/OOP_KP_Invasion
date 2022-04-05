using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;
using Transform = Invasion.Engine.Components.Transform;

namespace Invasion.View;

public class HealthView : IView
{
    private Transform _parent;
    private GameObject _gameObject;
    private IHealthable _healthable;
    private TextRenderer _healthText;

    public HealthView(GameObject healthableObject, WindowRenderTarget renderTarget)
    {
        _healthable = healthableObject as IHealthable;
        if (_healthable is null)
        {
            throw new Exception();
        }

        healthableObject.TryTakeComponent(out _parent);
        _healthText = new TextRenderer(renderTarget, new RectangleF());
        _healthText.SetText($"{_healthable.CurrentHealthPoints}/{_healthable.MaxHealthPoint} HP");
        _healthable.OnHealthChanged += ChangeText;
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