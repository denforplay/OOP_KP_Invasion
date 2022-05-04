using Invasion.Engine.Graphics;
using Invasion.Engine.Graphics.GraphicTargets;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using SharpDX.Windows;

namespace Invasion.Engine
{
    public class DirectXGraphicsProvider : IGraphicProvider
    {
        public Factory Factory { get; init; }
        public SharpDX.DirectWrite.Factory WriteFactory { get; init; }
        public ImagingFactory ImagingFactory { get; init; }
        public Dictionary<string, System.Drawing.Bitmap> BitmapsConfiguration { get; set; } = new Dictionary<string, System.Drawing.Bitmap>();
        public IGraphicTarget GraphicTarget { get; set; }

        public DirectXGraphicsProvider(RenderForm form)
        {
            BitmapsConfiguration = new Dictionary<string, System.Drawing.Bitmap>();
            Factory = new Factory();
            WriteFactory = new SharpDX.DirectWrite.Factory();
            RenderTargetProperties renderProp = new RenderTargetProperties()
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = FeatureLevel.Level_DEFAULT,
                PixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Default,
                Usage = RenderTargetUsage.None,
            };

            HwndRenderTargetProperties winProp = new HwndRenderTargetProperties()
            {
                Hwnd = form.Handle,
                PixelSize = new Size2(Screen.Width, Screen.Height),
                PresentOptions = PresentOptions.None
            };
            GraphicTarget = new SharpDXTarget(new WindowRenderTarget(Factory, renderProp, winProp));
            ImagingFactory = new ImagingFactory();
        }

        public void Dispose()
        {
            ImagingFactory.Dispose();
            Factory.Dispose();
            WriteFactory.Dispose();
            foreach (var bitmap in BitmapsConfiguration.Values)
                bitmap.Dispose();
        }

        public void LoadBitmap(string filePath)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filePath);
            BitmapsConfiguration.Add(filePath, bitmap);
        }
    }
}
