using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.WIC;
using SharpDX.Windows;
using System.Windows;
using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace Invasion.Engine
{
    public class DX2D
    {
        private SharpDX.Direct2D1.Factory _factory;
        public SharpDX.Direct2D1.Factory Factory { get => _factory; }

        private SharpDX.DirectWrite.Factory _writeFactory;
        public SharpDX.DirectWrite.Factory WriteFactory { get => _writeFactory; }
        private WindowRenderTarget _renderTarget;
        public WindowRenderTarget RenderTarget { get => _renderTarget; }

        private ImagingFactory _imagingFactory;
        public ImagingFactory ImagingFactory { get => _imagingFactory; }

        private Dictionary<string, SharpDX.Direct2D1.Bitmap> _bitmaps;
        public Dictionary<string, SharpDX.Direct2D1.Bitmap> Bitmaps { get => _bitmaps; }

        public DX2D(RenderForm form, Window window)
        {
            _bitmaps = new Dictionary<string, Bitmap>();
            _factory = new SharpDX.Direct2D1.Factory();
            _writeFactory = new SharpDX.DirectWrite.Factory();
            RenderTargetProperties renderProp = new RenderTargetProperties()
            {
                DpiX = 96,
                DpiY = 96,
                MinLevel = FeatureLevel.Level_DEFAULT,
                PixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Default,
                Usage = RenderTargetUsage.None,
            };

            HwndRenderTargetProperties winProp = new HwndRenderTargetProperties()
            {
                Hwnd = form.Handle,
                PixelSize = new Size2((int)Screen.Width/*window.ActualWidth*/, (int)Screen.Height/*window.ActualHeight*/),
                PresentOptions = PresentOptions.None                                      // Immediately // None - vSync
            };
            _renderTarget = new WindowRenderTarget(_factory, renderProp, winProp);
            _imagingFactory = new ImagingFactory();
        }

        public int LoadBitmap(string imageFileName)
        {
            BitmapDecoder decoder = new BitmapDecoder(_imagingFactory, imageFileName, DecodeOptions.CacheOnDemand);
            BitmapFrameDecode frame = decoder.GetFrame(0);
            FormatConverter converter = new FormatConverter(_imagingFactory);
            converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA, BitmapDitherType.Ordered4x4, null, 0.0, BitmapPaletteType.Custom);
            SharpDX.Direct2D1.Bitmap bitmap = SharpDX.Direct2D1.Bitmap.FromWicBitmap(_renderTarget, converter);

            Utilities.Dispose(ref converter);
            Utilities.Dispose(ref frame);
            Utilities.Dispose(ref decoder);

            _bitmaps.Add(imageFileName, bitmap);
            return _bitmaps.Count - 1;
        }

        public void Dispose()
        {
            foreach (var value in _bitmaps.Values)
            {
                SharpDX.Direct2D1.Bitmap bitmap = value;
                Utilities.Dispose(ref bitmap);
            }
                
            Utilities.Dispose(ref _imagingFactory);
            Utilities.Dispose(ref _renderTarget);
            Utilities.Dispose(ref _factory);
        }
    }
}
