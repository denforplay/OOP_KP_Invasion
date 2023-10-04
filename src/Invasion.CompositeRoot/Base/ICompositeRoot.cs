namespace Invastion.CompositeRoot.Base
{
    /// <summary>
    /// Composite root base interface
    /// </summary>
    public interface ICompositeRoot : IDisposable
    {
        /// <summary>
        /// Compose low chained entities
        /// </summary>
        void Compose();
    }
}
