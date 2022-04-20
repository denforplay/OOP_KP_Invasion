using Invasion.Engine.Graphics.GraphicTargets;

namespace Invasion.Engine.Graphics
{
    public interface IGraphicProvider
    {
       IGraphicTarget GraphicTarget { get; set; }
       Dictionary<string, Bitmap> BitmapsConfiguration { get; set; }
       void LoadBitmap(string filePath);
    }
}