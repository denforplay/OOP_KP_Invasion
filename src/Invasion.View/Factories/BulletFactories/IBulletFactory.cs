using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.View.Factories.BulletFactories;

public interface IBulletFactory
{
    public BulletBase Create();
}