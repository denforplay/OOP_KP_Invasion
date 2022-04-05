using Invasion.Engine.Interfaces;

namespace Invasion.Core
{
    public sealed class Record<T1, T2> : IRecord
    {
        private readonly Action<T1, T2> _action;

        public Record(Action<T1, T2> action)
        {
            _action = action;
        }

        public void Do(T1 modelA, T2 modelB)
        {
            _action.Invoke(modelA, modelB);
        }

        public void Do(T2 modelB, T1 modelA)
        {
            _action.Invoke(modelA, modelB);
        }

        public bool IsTarget((object, object) pair)
        {
            return (pair.Item1 is T1 && pair.Item2 is T2)
                   || (pair.Item1 is T2 && pair.Item2 is T1);
        }
    }
}
