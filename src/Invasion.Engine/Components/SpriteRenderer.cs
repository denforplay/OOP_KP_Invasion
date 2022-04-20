using Invasion.Engine.Components.Interfaces;
using Invasion.Engine.Graphics;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Invasion.Engine.Components
{
    public class SpriteRenderer : IRenderer
    {
        private readonly float _pu = 1.0f / 16.0f;

        private IGraphicProvider _graphicProvider;
        private Transform _transform;
        public Size Size => _bitmapSize;
        private Size _bitmapSize;
        private Vector2 _bitmapCenter;
        private Vector2 _translation;
        private Matrix3x2 _matrix;
        private RendererMode _mode;
        private SharpDX.Direct2D1.Bitmap _bitmap;

        public SpriteRenderer(IGraphicProvider graphicProvider, string spriteFileName, RendererMode rendererMode = RendererMode.Dynamic)
        {
            _transform = new Transform();
            _mode = rendererMode;
            _transform.Scale = new System.Numerics.Vector3(1, 1, 1);
            _graphicProvider = graphicProvider;
            _bitmap = LoadFrom(_graphicProvider.BitmapsConfiguration[spriteFileName]);
            _bitmapSize = new Size((int)_bitmap.Size.Width, (int)_bitmap.Size.Height);
            _bitmapCenter.X = _bitmapSize.Width / 2.0f;
            _bitmapCenter.Y = _bitmapSize.Height / 2.0f;
        }

        private SharpDX.Direct2D1.Bitmap LoadFrom(System.Drawing.Bitmap bitmap)
        {
            var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
            var size = new Size2(bitmap.Width, bitmap.Height);

            int stride = bitmap.Width * sizeof(int);
            using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
            {
                var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    int offset = bitmapData.Stride * y;
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                        int rgba = R | (G << 8) | (B << 16) | (A << 24);
                        tempStream.Write(rgba);
                    }

                }
                bitmap.UnlockBits(bitmapData);
                tempStream.Position = 0;
                return new SharpDX.Direct2D1.Bitmap(_graphicProvider.GraphicTarget.Target, size, tempStream, stride, bitmapProperties);
            }
        }

        public virtual void Draw()
        {
            float height = Screen.Height;
            float scale = Screen.Height / Screen.UnitsPerHeight;
            _translation.X = (-_bitmapCenter.X * _pu + _transform.Position.X) * scale;
            _translation.Y = height - (-_bitmapCenter.Y * _pu + _transform.Position.Y + 1) * scale;
            if (_mode == RendererMode.Static)
                _translation = Vector2.Zero;
            _matrix =
                Matrix3x2.Rotation(_transform.Rotation.X, _bitmapCenter)*
                Matrix3x2.Scaling(scale * _pu, scale * _pu, Vector2.Zero) *
                Matrix3x2.Translation(_translation) ;

            _graphicProvider.GraphicTarget.Target.Transform = _matrix;
            _graphicProvider.GraphicTarget.Target.DrawBitmap(_bitmap, 1, BitmapInterpolationMode.Linear);
        }

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }
    }
}
