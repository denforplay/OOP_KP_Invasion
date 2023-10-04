using Invasion.Engine;
using Invasion.Models;

namespace Invasion.View.Factories.Base;

/// <summary>
/// Class provides game object view factory
/// </summary>
/// <typeparam name="T">Type of gameobject to create view</typeparam>
public class GameObjectViewFactoryBase<T> where T : GameObject
{
    private Queue<GameObjectView> _entitiesViews = new Queue<GameObjectView>();

    /// <summary>
    /// Create new game object view from entity
    /// </summary>
    /// <param name="entity">Entity of view</param>
    /// <returns>New gameobject view</returns>
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