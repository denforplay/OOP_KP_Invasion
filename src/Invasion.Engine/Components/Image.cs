﻿using Invasion.Core.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.Engine.Components
{
    public class Image : IComponent
    {
        private Vector2 _translation;
        private Matrix3x2 _matrix;
        private Vector2 _bitmapCenter;
        private DX2D _dx2d;
        private int _bitmapIndex;

        private Transform _transform = new Transform();//!!!HARDCODE(VALUE MUST BE INITIALIZED FROM CONSTRUCTOR
        public Transform Transform => _transform;

        public Image(DX2D dx2d, int bitmapIndex, Vector3 position, Vector3 rotation)
        {
            _dx2d = dx2d;
            _bitmapIndex = bitmapIndex;
            Transform.Position = position;
            Transform.Rotation = rotation;
            Transform.Scale = new Vector3(1, 1, 1);
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_bitmapIndex];
            _bitmapCenter.X = bitmap.Size.Width / 2.0f;
            _bitmapCenter.Y = bitmap.Size.Height / 2.0f;
        }

        public void DrawBackground(float opacity, float scale, float textureScale, float height)
        {
            _translation.X = Transform.Position.X * scale;
            _translation.Y = -Transform.Position.Y * scale;
            _matrix = Matrix3x2.Rotation(-Transform.Rotation.X, _bitmapCenter) *
                Matrix3x2.Scaling(scale * textureScale, scale * textureScale, Vector2.Zero) *
                Matrix3x2.Translation(_translation);
            WindowRenderTarget r = _dx2d.RenderTarget;
            r.Transform = _matrix;
            SharpDX.Direct2D1.Bitmap bitmap = _dx2d.Bitmaps[_bitmapIndex];
            r.DrawBitmap(bitmap, opacity, BitmapInterpolationMode.Linear);
        }
    }
}
