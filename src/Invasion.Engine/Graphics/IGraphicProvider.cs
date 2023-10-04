using Invasion.Engine.Graphics.GraphicTargets;

namespace Invasion.Engine.Graphics
{
    /// <summary>
    /// Provides graphics functionality
    /// </summary>
    public interface IGraphicProvider : IDisposable
    {
        /// <summary>
        /// Graphic target
        /// </summary>
        IGraphicTarget GraphicTarget { get; set; }

        /// <summary>
        /// Bitmaps configuration
        /// </summary>
        Dictionary<string, Bitmap> BitmapsConfiguration { get; set; }

        /// <summary>
        /// Load bitmap from filepath
        /// </summary>
        /// <param name="filePath">Bitmap file path</param>
        void LoadBitmap(string filePath);
    }
}