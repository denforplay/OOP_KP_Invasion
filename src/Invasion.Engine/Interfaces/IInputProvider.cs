using System.Windows.Input;

namespace Invasion.Engine.Interfaces
{
    public interface IInputProvider
    {
        bool CheckKey(Key key);
    }
}
