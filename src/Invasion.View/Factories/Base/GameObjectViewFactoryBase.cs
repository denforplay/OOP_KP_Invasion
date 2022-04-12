using Invasion.Engine;
using Invasion.Models;

namespace Invasion.View.Factories.Base;

public abstract class GameObjectViewFactoryBase<T> where T : GameObject
{
    private Queue<GameObjectView> _entitiesViews = new Queue<GameObjectView>();

    public GameObjectView Create(Entity<T> entity)
    {
        GameObjectView view = new GameObjectView(entity.GetEntity);
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