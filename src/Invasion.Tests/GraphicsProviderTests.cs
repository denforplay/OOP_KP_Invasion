using Invasion.Engine;
using Invasion.Engine.Graphics;
using Moq;
using SharpDX.Windows;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Invasion.Tests
{
    public class GraphicsProviderTests
    {
        private IGraphicProvider _graphicProvider;

        public GraphicsProviderTests()
        {
            _graphicProvider = new DirectXGraphicsProvider(new RenderForm());
        }

        [Fact]
        public void LoadBitmap_ValidBitmap_ReturnsTrue()
        {
            _graphicProvider.LoadBitmap("test.bmp");
            Assert.True(_graphicProvider.BitmapsConfiguration.ContainsKey("test.bmp"));
        }
    }
}
