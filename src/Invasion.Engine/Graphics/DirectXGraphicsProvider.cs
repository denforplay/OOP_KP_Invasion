using Invasion.Engine.Graphics;
using Invasion.Engine.Graphics.GraphicTargets;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using SharpDX.Windows;
using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace Invasion.Engine
{
    public class DirectXGraphicsProvider : IGraphicProvider
    {
        private Factory _factory;
        public Factory Factory { get => _factory; }

        private SharpDX.DirectWrite.Factory _writeFactory;
        public SharpDX.DirectWrite.Factory WriteFactory { get => _writeFactory; }
        private WindowRenderTarget _renderTarget;
        public WindowRenderTarget RenderTarget { get => _renderTarget; }

        private ImagingFactory _imagingFactory;
        public ImagingFactory ImagingFactory { get => _imagingFactory; }
        public Dictionary<string, System.Drawing.Bitmap> BitmapsConfiguration { get; set; } = new Dictionary<string, System.Drawing.Bitmap>();
        public IGraphicTarget GraphicTarget { get; set; }

        public DirectXGraphicsProvider(RenderForm form)
        {
            BitmapsConfiguration = new Dictionary<string, System.Drawing.Bitmap>();
            _factory = new Factory();
            _writeFactory = new SharpDX.DirectWrite.Factory();
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
            GraphicTarget = new SharpDXTarget(new WindowRenderTarget(_factory, renderProp, winProp));
            _imagingFactory = new ImagingFactory();
        }

        public void Dispose()
        {
            Utilities.Dispose(ref _imagingFactory);
            Utilities.Dispose(ref _renderTarget);
            Utilities.Dispose(ref _factory);
            Utilities.Dispose(ref _writeFactory);
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
