using SharpDX.Direct2D1;

namespace Invasion.Engine.Graphics.GraphicTargets
{
    public class SharpDXTarget : IGraphicTarget
    {
        public dynamic Target { get; set; }

        public SharpDXTarget(WindowRenderTarget windowRenderTarget)
        {
            Target = windowRenderTarget;
        }
    }
}
