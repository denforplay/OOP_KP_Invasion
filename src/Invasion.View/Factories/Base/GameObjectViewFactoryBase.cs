using Invasion.Core;
using Invasion.Engine;

namespace Invasion.View.Factories.Base;

public abstract class GameObjectViewFactoryBase<T> where T : GameObject
{
    private Queue<GameObjectView> _entitiesViews = new Queue<GameObjectView>();
    protected DX2D _dx2D;

    public GameObjectViewFactoryBase(DX2D dx2D)
    {
        _dx2D = dx2D;
    }

    public GameObjectView Create(Entity<T> entity, float scale, float height)
    {
        GameObjectView view = new GameObjectView(entity.GetEntity, scale, height);
        _entitiesViews.Enqueue(view);
        return view;
    }
    
    public void Destroy(Entity<T> entity)
    {
        if (_entitiesViews.Count > 0)
        {
            _entitiesViews.Dequeue();
        }
    }
}