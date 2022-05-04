using Invasion.Engine.Interfaces;

namespace Invasion.Core
{
    /// <summary>
    /// Represents record
    /// </summary>
    /// <typeparam name="T1">First type</typeparam>
    /// <typeparam name="T2">Second type</typeparam>
    public sealed class Record<T1, T2> : IRecord
    {
        private readonly Action<T1, T2> _action;

        /// <summary>
        /// Record constructor
        /// </summary>
        /// <param name="action">Action to do with object of types t1 and t2</param>
        public Record(Action<T1, T2> action)
        {
            _action = action;
        }

        /// <summary>
        /// Apply action to objects
        /// </summary>
        /// <param name="modelA">First object</param>
        /// <param name="modelB">Second object</param>
        public void Do(T1 modelA, T2 modelB)
        {
            _action.Invoke(modelA, modelB);
        }

        /// <summary>
        /// Apply action to objects
        /// </summary>
        /// <param name="modelA">Second object</param>
        /// <param name="modelB">First object</param>
        public void Do(T2 modelB, T1 modelA)
        {
            _action.Invoke(modelA, modelB);
        }

        /// <summary>
        /// Method to check if pair is target
        /// </summary>
        /// <param name="pair">Pair of two objects</param>
        /// <returns>True if object pair is target other returns false</returns>
        public bool IsTarget((object, object) pair)
        {
            return (pair.Item1 is T1 && pair.Item2 is T2)
                   || (pair.Item1 is T2 && pair.Item2 is T1);
        }
    }
}
