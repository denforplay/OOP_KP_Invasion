using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using Brush = SharpDX.Direct2D1.Brush;
using Color = SharpDX.Color;
using Factory = SharpDX.DirectWrite.Factory;
using RectangleF = SharpDX.RectangleF;
using Invasion.Engine.Interfaces;

namespace Invasion.Engine.Components;

public class TextRenderer : IComponent
{
    private Factory _writeFactory;
    private TextFormat _textFormat;
    private Brush _whiteBrush;
    private WindowRenderTarget _renderTarget;
    private RectangleF _position;
    private float _fontSize;
    private string _text = "fps";
        
    public TextRenderer(WindowRenderTarget renderTarget, RectangleF position, float fontSize = 15)
    {
        _fontSize = fontSize;
        _position = position;
        _renderTarget = renderTarget;
        _writeFactory = new SharpDX.DirectWrite.Factory();
        _textFormat = new TextFormat(_writeFactory, "Valorant", _fontSize)
        {
            TextAlignment = TextAlignment.Center,
        };
        _whiteBrush = new SolidColorBrush(_renderTarget, Color.White);
    }

    public void SetPosition(RectangleF position)
    {
        _position = position;
    }
    
    public void SetText(string text)
    {
        _text = text;
    }

    public void Update()
    {
        _renderTarget.Transform = Matrix3x2.Identity;
        _renderTarget.DrawText(_text,
            _textFormat, _position, _whiteBrush);
    }
}