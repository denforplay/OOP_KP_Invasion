using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using Brush = SharpDX.Direct2D1.Brush;
using Color = SharpDX.Color;
using Factory = SharpDX.DirectWrite.Factory;
using RectangleF = SharpDX.RectangleF;
using Invasion.Engine.Interfaces;

namespace Invasion.Engine.Components;

/// <summary>
/// Represents text renderer
/// </summary>
public class TextRenderer : IComponent, IDisposable
{
    private Factory _writeFactory;
    private TextFormat _textFormat;
    private Brush _whiteBrush;
    private WindowRenderTarget _renderTarget;
    private RectangleF _position;
    private float _fontSize;
    private string _text;

    /// <summary>
    /// Text renderer constructor
    /// </summary>
    /// <param name="renderTarget">Render target</param>
    /// <param name="position">Text position</param>
    /// <param name="fontSize">Font size</param>
    public TextRenderer(WindowRenderTarget renderTarget, RectangleF position, float fontSize = 15)
    {
        _fontSize = fontSize;
        _position = position;
        _renderTarget = renderTarget;
        _writeFactory = new Factory();
        _textFormat = new TextFormat(_writeFactory, "Valorant", _fontSize)
        {
            TextAlignment = TextAlignment.Center,
        };
        _whiteBrush = new SolidColorBrush(_renderTarget, Color.White);
    }

    /// <summary>
    /// Set new text position method
    /// </summary>
    /// <param name="position">New text position</param>
    public void SetPosition(RectangleF position)
    {
        _position = position;
    }
    
    /// <summary>
    /// Set new text
    /// </summary>
    /// <param name="text">Text</param>
    public void SetText(string text)
    {
        _text = text;
    }

    /// <summary>
    /// Update this text renderer
    /// </summary>
    public void Update()
    {
        _renderTarget.Transform = Matrix3x2.Identity;
        _renderTarget.DrawText(_text,
            _textFormat, _position, _whiteBrush);
    }

    public void Dispose()
    {
        Utilities.Dispose(ref _writeFactory);
        Utilities.Dispose(ref _whiteBrush);
        Utilities.Dispose(ref _textFormat);
    }
}