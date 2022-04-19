namespace Invastion.CompositeRoot.Base
{
    /// <summary>
    /// Composite root base interface
    /// </summary>
    public interface ICompositeRoot
    {
        /// <summary>
        /// Compose low chained entities
        /// </summary>
        void Compose();

        /// <summary>
        /// Dispose entities
        /// </summary>
        void Dispose();
    }
}
