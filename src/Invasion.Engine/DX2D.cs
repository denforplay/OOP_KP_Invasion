﻿using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.WIC;
using SharpDX.Windows;
using System.Windows;
using System.Windows.Interop;

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

        // Формат текста для вывода статистики в верхнем левом углу
        private TextFormat _textFormatStats;
        public TextFormat TextFormatStats { get => _textFormatStats; }
        // Формат текста для сообщения по центру окна (пока не используется)
        private TextFormat _textFormatMessage;
        public TextFormat TextFormatMessage { get => _textFormatStats; }
        private SharpDX.Direct2D1.Brush _redBrush;
        public SharpDX.Direct2D1.Brush RedBrush { get => _redBrush; }
        private SharpDX.Direct2D1.Brush _whiteBrush;
        public SharpDX.Direct2D1.Brush WhiteBrush { get => _whiteBrush; }

        private List<SharpDX.Direct2D1.Bitmap> _bitmaps;
        public List<SharpDX.Direct2D1.Bitmap> Bitmaps { get => _bitmaps; }

        public DX2D(RenderForm form, Window window)
        {
            _factory = new SharpDX.Direct2D1.Factory();
            _writeFactory = new SharpDX.DirectWrite.Factory();
            RenderTargetProperties renderProp = new RenderTargetProperties()
            {
                DpiX = 96,
                DpiY = 96,
                MinLevel = FeatureLevel.Level_DEFAULT,
                PixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None,
            };

            HwndRenderTargetProperties winProp = new HwndRenderTargetProperties()
            {
                Hwnd = form.Handle,
                PixelSize = new Size2((int)window.ActualWidth, (int)window.ActualHeight),
                PresentOptions = PresentOptions.None                                      // Immediately // None - vSync
            };
            _renderTarget = new WindowRenderTarget(_factory, renderProp, winProp);
            _imagingFactory = new ImagingFactory();
            form.IsFullscreen = true;
            _textFormatStats = new TextFormat(_writeFactory, "Calibri", 12);
            _textFormatStats.ParagraphAlignment = ParagraphAlignment.Near;
            _textFormatStats.TextAlignment = SharpDX.DirectWrite.TextAlignment.Leading;
            _textFormatMessage = new TextFormat(_writeFactory, "Calibri", 24);
            _textFormatMessage.ParagraphAlignment = ParagraphAlignment.Center;
            _textFormatMessage.TextAlignment = SharpDX.DirectWrite.TextAlignment.Center;
            _redBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Red);
            _whiteBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.White);
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

            if (_bitmaps == null) _bitmaps = new List<SharpDX.Direct2D1.Bitmap>(4);
            _bitmaps.Add(bitmap);
            return _bitmaps.Count - 1;
        }

        public void Dispose()
        {
            for (int i = _bitmaps.Count - 1; i >= 0; i--)
            {
                SharpDX.Direct2D1.Bitmap bitmap = _bitmaps[i];
                _bitmaps.RemoveAt(i);
                Utilities.Dispose(ref bitmap);
            }
            Utilities.Dispose(ref _whiteBrush);
            Utilities.Dispose(ref _redBrush);
            Utilities.Dispose(ref _textFormatStats);
            Utilities.Dispose(ref _imagingFactory);
            Utilities.Dispose(ref _renderTarget);
            Utilities.Dispose(ref _factory);
        }
    }
}