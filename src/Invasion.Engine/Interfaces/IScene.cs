using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invasion.Engine.Interfaces
{
    /// <summary>
    /// Provides scene functionality
    /// </summary>
    public interface IScene
    {
        void Add(GameObject gameObject, IView view, IController controller);
        void AddController(IController controller);
        void RemoveController(IController controller);
        void AddGameObject(GameObject gameObject);
        void RemoveGameObject(GameObject gameObject);
        void AddView(IView view);
        void RemoveView(IView view);

    }
}
