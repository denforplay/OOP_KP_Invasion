using Invasion.Engine.Graphics;
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

        private Dictionary<string, Bitmap> _bitmaps;
        public Dictionary<string, Bitmap> Bitmaps { get => _bitmaps; }

        public DirectXGraphicsProvider(RenderForm form)
        {
            _bitmaps = new Dictionary<string, Bitmap>();
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
                PixelSize = new Size2((int)Screen.Width, (int)Screen.Height),
                PresentOptions = PresentOptions.None                                      // Immediately // None - vSync
            };
            _renderTarget = new WindowRenderTarget(_factory, renderProp, winProp);
            _imagingFactory = new ImagingFactory();
        }

        public void Dispose()
        {
            foreach (var value in _bitmaps.Values)
            {
                Bitmap bitmap = value;
                Utilities.Dispose(ref bitmap);
            }
                
            Utilities.Dispose(ref _imagingFactory);
            Utilities.Dispose(ref _renderTarget);
            Utilities.Dispose(ref _factory);
        }

        public void LoadBitmap(string filePath)
        {
            BitmapDecoder decoder = new BitmapDecoder(_imagingFactory, filePath, DecodeOptions.CacheOnDemand);
            BitmapFrameDecode frame = decoder.GetFrame(0);
            FormatConverter converter = new FormatConverter(_imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA, BitmapDitherType.Ordered4x4, null, 0.0, BitmapPaletteType.Custom);
            Bitmap bitmap = Bitmap.FromWicBitmap(_renderTarget, converter);
            Utilities.Dispose(ref converter);
            Utilities.Dispose(ref frame);
            Utilities.Dispose(ref decoder);
            _bitmaps.Add(filePath, bitmap);
        }
    }
}
